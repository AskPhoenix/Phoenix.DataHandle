using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using Phoenix.Bot.Dialogs.Student;
using System;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.Bot.Dialogs.Teacher;
using static Phoenix.Bot.Helpers.DialogHelper;
using Phoenix.Bot.Helpers;

namespace Phoenix.Bot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly IConfiguration _configuration;
        private readonly BotState _conversationState;
        private readonly BotState _userState;

        private static class WaterfallNames
        {
            public const string Main = "Main_WaterfallDialog";
        }

        public MainDialog(IConfiguration configuration, ConversationState conversationState, UserState userState,
            AuthDialog authDialog, WelcomeDialog welcomeDialog, FeedbackDialog feedbackDialog, StudentDialog studentDialog, TeacherDialog teacherDialog)
            : base(nameof(MainDialog))
        {
            _configuration = configuration;
            _conversationState = conversationState;
            _userState = userState;

            AddDialog(authDialog);
            AddDialog(welcomeDialog);
            AddDialog(feedbackDialog);
            AddDialog(studentDialog);
            AddDialog(teacherDialog);

            AddDialog(new WaterfallDialog(WaterfallNames.Main,
                new WaterfallStep[]
                {
                    FirstTimeStepAsync,
                    UserRegisterStepAsync,
                    CommandHandleStepAsync,
                    GreetingStepAsync,
                    ForwardStepAsync,
                    LoopStepAsync
                }));

            InitialDialogId = WaterfallNames.Main;
        }

        #region Main Waterfall Dialog

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

            return await stepContext.BeginDialogAsync(nameof(WelcomeDialog), null, cancellationToken);
        }

        private async Task<DialogTurnResult> CommandHandleStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            Persistent.TryGetCommand(stepContext.Context.Activity.Text, out Persistent.Command cmd);
            switch (cmd)
            {
                case Persistent.Command.GetStarted:
                case Persistent.Command.Tutorial:
                    return await stepContext.BeginDialogAsync(nameof(WelcomeDialog), null, cancellationToken);
                case Persistent.Command.Feedback:
                    return await stepContext.BeginDialogAsync(nameof(FeedbackDialog), null, cancellationToken);
                default:
                    return await stepContext.NextAsync(null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> GreetingStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string mess = stepContext.Context.Activity.Text;
            if (!mess.ContainsSynonyms(SynonymHelper.Topics.Greetings))
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
            => await stepContext.ReplaceDialogAsync(stepContext.ActiveDialog.Id, stepContext.Options, cancellationToken);

        #endregion
    }
}
