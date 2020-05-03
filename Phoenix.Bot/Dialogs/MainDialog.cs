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
using Phoenix.DataHandle.Main.Models;
using System.Linq;
using Phoenix.DataHandle.Main;

namespace Phoenix.Bot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly IConfiguration _configuration;
        private readonly BotState _conversationState;
        private readonly BotState _userState;
        private readonly PhoenixContext _phoenixContext;

        private static class WaterfallNames
        {
            public const string Main = "Main_WaterfallDialog";
        }

        public MainDialog(IConfiguration configuration, ConversationState conversationState, UserState userState, PhoenixContext phoenixContext,
            AuthDialog authDialog, WelcomeDialog welcomeDialog, FeedbackDialog feedbackDialog, StudentDialog studentDialog, TeacherDialog teacherDialog)
            : base(nameof(MainDialog))
        {
            _configuration = configuration;
            _conversationState = conversationState;
            _userState = userState;
            _phoenixContext = phoenixContext;

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

            var phoneAcsr = _conversationState.CreateProperty<string>("Phone");
            string phone = await phoneAcsr.GetAsync(stepContext.Context);
            await phoneAcsr.DeleteAsync(stepContext.Context);

            //This is for the students and their parents who have registered with the same phone number
            var codeAcsr = _conversationState.CreateProperty<string>("OneTimeCode");
            string code = await codeAcsr.GetAsync(stepContext.Context);
            await phoneAcsr.DeleteAsync(stepContext.Context);

            var user = _phoenixContext.AspNetUsers.SingleOrDefault(u => u.PhoneNumber == phone && u.OneTimeCode == code);
            user.FacebookId = stepContext.Context.Activity.From.Id;
            if (!string.IsNullOrEmpty(code))
                user.OneTimeCodeUsed = true;
            await _phoenixContext.SaveChangesAsync();

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

            var name = _phoenixContext.User.Single(u => u.AspNetUser.FacebookId == stepContext.Context.Activity.From.Id).FirstName;
            reply = MessageFactory.Text($"Γεια σου {GreekNameCall(name)}! 😊");
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> ForwardStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //TODO: Allow to choose between roles if has multiple
            var userRole = (Role)_phoenixContext.AspNetRoles.
                First(r => r.AspNetUserRoles.Single(ur => ur.User.FacebookId == stepContext.Context.Activity.From.Id).RoleId == r.Id).Type;

            return userRole switch
            {
                Role.Student => await stepContext.BeginDialogAsync(nameof(StudentDialog), null, cancellationToken),
                Role.Teacher => await stepContext.BeginDialogAsync(nameof(TeacherDialog), null, cancellationToken),
                _ => await stepContext.CancelAllDialogsAsync(cancellationToken)
            };
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.ReplaceDialogAsync(stepContext.ActiveDialog.Id, stepContext.Options, cancellationToken);

        #endregion
    }
}
