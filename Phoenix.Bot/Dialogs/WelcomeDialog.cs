using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using static Phoenix.Bot.Helpers.DialogHelper;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs
{
    public class WelcomeDialog : ComponentDialog
    {
        private static class WaterfallNames
        {
            public const string AskForTutorial  = "AskForTutorial_WaterfallDialog";
            public const string Tutorial        = "Tutorial_WaterfallDialog";
        }

        public WelcomeDialog()
            : base(nameof(WelcomeDialog))
        {
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));

            AddDialog(new WaterfallDialog(WaterfallNames.AskForTutorial,
                new WaterfallStep[]
                {
                    AskStepAsync,
                    ReplyStepAsync,
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Tutorial,
                new WaterfallStep[]
                {
                    StartTutorialStepAsync,
                    FinalStepAsync
                }));
        }

        protected override async Task<DialogTurnResult> OnBeginDialogAsync(DialogContext innerDc, object options, CancellationToken cancellationToken = default)
        {
            string mess = innerDc.Context.Activity.Text;
            Persistent.TryGetCommand(mess, out Persistent.Command cmd);

            InitialDialogId = WaterfallNames.AskForTutorial;

            if (cmd == Persistent.Command.Tutorial)
            {
                await innerDc.Context.SendActivityAsync(MessageFactory.Text("Ας κάνουμε μια σύντομη περιήγηση!"));
                InitialDialogId = WaterfallNames.Tutorial;
            }
            else if (cmd == Persistent.Command.GetStarted)
                await innerDc.Context.SendActivityAsync(MessageFactory.Text("Καλωσόρισες στο Phoenix! 😁"));

            return await base.OnBeginDialogAsync(innerDc, options, cancellationToken);
        }

        #region Ask for Tutorial Waterfall Dialog

        private async Task<DialogTurnResult> AskStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(
                nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Προτού ξεκινήσουμε, θα ήθελες να σου δείξω τι μπορώ να κάνω με μια σύντομη περιήγηση;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ απάντησε με ένα Ναι ή Όχι:"),
                    Choices = new Choice[] { new Choice("Ναι"), new Choice("Όχι") }
                });
        }

        private async Task<DialogTurnResult> ReplyStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var foundChoice = stepContext.Result as FoundChoice;
            if (foundChoice.Index == 0)
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Τέλεια! 😁"));
                return await stepContext.BeginDialogAsync(WaterfallNames.Tutorial, null, cancellationToken);
            }

            var reply = MessageFactory.Text("Έγινε, κανένα πρόβλημα! Ας ξεκινήσουμε λοιπόν!");
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        #endregion

        #region Tutorial Waterfall Dialog

        private async Task<DialogTurnResult> StartTutorialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //TODO: Create Tutorial
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("<Tutorial> ..."));

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        #endregion
    }
}
