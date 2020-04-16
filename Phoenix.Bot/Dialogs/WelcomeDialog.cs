using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs
{
    public class WelcomeDialog : ComponentDialog
    {
        private static class WaterfallNames
        {
            public const string Tutorial = "Tutorial_WaterfallDialog";
        }

        public WelcomeDialog()
            : base(nameof(WelcomeDialog))
        {
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));

            AddDialog(new WaterfallDialog(WaterfallNames.Tutorial,
                new WaterfallStep[]
                {
                    AskStepAsync,
                    ReplyStepAsync,
                    StartTutorialStepAsync,
                    FinalStepAsync
                }));

            InitialDialogId = WaterfallNames.Tutorial;
        }

        #region Tutorial Waterfall Dialog

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
                return await stepContext.NextAsync(null, cancellationToken);

            var reply = MessageFactory.Text("Έγινε, κανένα πρόβλημα! Ας ξεκινήσουμε λοιπόν!");
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> StartTutorialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Τέλεια! 😁");
            await stepContext.Context.SendActivityAsync(reply);

            //TODO: Create Tutorial
            reply.Text = "<Tutorial>";
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        #endregion
    }
}
