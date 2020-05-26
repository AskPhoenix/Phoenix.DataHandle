using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Phoenix.Bot.Dialogs.Student;
using Phoenix.Bot.Extensions;

namespace Phoenix.Bot.Dialogs.Teacher
{
    public class TeacherDialog : ComponentDialog
    {
        private static class WaterfallNames
        {
            public const string Menu = "Teacher_Menu_WaterfallDialog";
            public const string Help = "Teacher_Help_WaterfallDialog";
        }

        public TeacherDialog()
            : base(nameof(TeacherDialog))
        {
            AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));
            AddDialog(new WaterfallDialog(WaterfallNames.Menu,
                new WaterfallStep[]
                {
                    MenuStepAsync,
                    TaskStepAsync,
                    LoopStepAsync
                }));

            InitialDialogId = WaterfallNames.Menu;
        }

        private async Task<DialogTurnResult> MenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Options is int index)
                return await stepContext.NextAsync(
                    new FoundChoice()
                    {
                        Index = index,
                        Value = index switch { 0 => "Εργασίες", 1 => "Διαγωνίσματα", 2 => "Βαθμολογίες", 3 => "Πρόγραμμα", _ => string.Empty },
                        Score = 1.0f
                    },
                    cancellationToken);

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Πώς θα μπορούσα να σε βοηθήσω;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ή πληκτρολόγησε μία από τις παρακάτω απαντήσεις:"),
                    Choices = ChoiceFactory.ToChoices(new string[] { "Εργασίες", "Διαγωνίσματα", "Βαθμολογίες", "Πρόγραμμα" })
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> TaskStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.ReplaceDialogAsync(stepContext.ActiveDialog.Id, stepContext.Options, cancellationToken);
    }
}
