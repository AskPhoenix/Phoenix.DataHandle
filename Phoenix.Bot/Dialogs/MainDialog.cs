using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using Phoenix.Bot.Dialogs.Student;
using System;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.Bot.Dialogs.Teacher;
using static Phoenix.Bot.Extensions.DialogExtensions;
using Phoenix.Bot.Extensions;
using Phoenix.DataHandle.Identity;

namespace Phoenix.Bot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _appContext;
        protected readonly BotState _conversationState;
        protected readonly BotState _userState;

        public MainDialog(IConfiguration configuration, ApplicationDbContext appContext, ConversationState conversationState, UserState userState,
            AuthDialog authDialog, WelcomeDialog welcomeDialog, StudentDialog studentDialog, TeacherDialog teacherDialog)
            : base(nameof(MainDialog))
        {
            _configuration = configuration;
            _appContext = appContext;
            _conversationState = conversationState;
            _userState = userState;

            AddDialog(authDialog);
            AddDialog(welcomeDialog);
            AddDialog(studentDialog);
            AddDialog(teacherDialog);

            AddDialog(new WaterfallDialog(nameof(MainDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    FirstTimeStepAsync,
                    UserRegisterStepAsync,
                    GreetingStepAsync,
                    ForwardStepAsync,
                    LoopStepAsync
                }));

            InitialDialogId = nameof(MainDialog) + "_" + nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> FirstTimeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            bool isAuthenticated = await _userState.CreateProperty<bool>("IsAuthenticated").GetAsync(stepContext.Context);

            if (!isAuthenticated)
                return await stepContext.BeginDialogAsync(nameof(AuthDialog), null, cancellationToken);

            return await stepContext.NextAsync(isAuthenticated, cancellationToken);
        }

        private async Task<DialogTurnResult> UserRegisterStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (!(stepContext.Result is bool) || !(bool)stepContext.Result)
                return await stepContext.CancelAllDialogsAsync(cancellationToken);

            var isAuthAcsr = _userState.CreateProperty<bool>("IsAuthenticated");
            bool oldIsAuth = await isAuthAcsr.GetAsync(stepContext.Context);
            if (oldIsAuth)
                return await stepContext.NextAsync(null, cancellationToken);

            await isAuthAcsr.SetAsync(stepContext.Context, true);

            var checkedPhoneAcsr = _conversationState.CreateProperty<string>("CheckedPhone");
            string checkedPhone = await checkedPhoneAcsr.GetAsync(stepContext.Context);
            await checkedPhoneAcsr.DeleteAsync(stepContext.Context);

            //TODO: Register user's FB ID to DB
            //var user = await _appContext.Users.SingleAsync(u => u.PhoneNumber == checkedPhone);
            //user.FacebookId = stepContext.Context.Activity.From.Id;

            stepContext.Values.Add("needsWelcome", true);

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> GreetingStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string mess = stepContext.Context.Activity.Text;
            if ((stepContext.Values.TryGetValue("needsWelcome", out object needsWelcome) && (bool)needsWelcome) || mess == "--persistent-get-started--")
            {
                stepContext.Values.Remove("needsWelcome");
                return await stepContext.BeginDialogAsync(nameof(WelcomeDialog), null, cancellationToken);
            }

            if (!mess.ContainsSynonyms(SynonymsExtensions.Topics.Greetings))
                return await stepContext.NextAsync(null, cancellationToken);

            var reply = MessageFactory.ContentUrl(
                url: await ReceiveGifAsync("g", "hi", 10, new Random().Next(10), _configuration["GiphyKey"]),
                contentType: "image/gif");
            await stepContext.Context.SendActivityAsync(reply);

            //TODO: Use DB User name
            reply = MessageFactory.Text($"Γεια σου {GreekNameCall(stepContext.Context.Activity.From.Name.Split(' ')[0])}! 😊");
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> ForwardStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //TODO: Find user role
            string userType = "student";

            return userType switch
            {
                "student" => await stepContext.BeginDialogAsync(nameof(StudentDialog), null, cancellationToken),
                "teacher" => await stepContext.BeginDialogAsync(nameof(TeacherDialog), null, cancellationToken),
                _ => await stepContext.CancelAllDialogsAsync(cancellationToken)
            };
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.ReplaceDialogAsync(stepContext.ActiveDialog.Id, stepContext.Options, cancellationToken);
        }
    }
}
