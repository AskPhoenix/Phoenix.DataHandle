using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs.Student
{
    public class StudentDialog : ComponentDialog
    {
        private static class WaterfallNames
        {
            public const string Menu = "StudentMenu_WaterfallDialog";
        }

        public StudentDialog(ExerciseDialog exerciseDialog, ExamDialog examDialog, ScheduleDialog scheduleDialog)
            : base(nameof(StudentDialog))
        {
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));

            AddDialog(exerciseDialog);
            AddDialog(examDialog);
            AddDialog(scheduleDialog);

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
            return await stepContext.PromptAsync(
                nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Πώς θα μπορούσα να σε βοηθήσω;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ή πληκτρολόγησε μία από τις παρακάτω απαντήσεις:"),
                    Choices = ChoiceFactory.ToChoices(new string[] { "Εργασίες", "Διαγωνίσματα", "Πρόγραμμα" })
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> TaskStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return (stepContext.Result as FoundChoice).Index switch
            {
                0 => await stepContext.BeginDialogAsync(nameof(ExerciseDialog), null, cancellationToken),
                1 => await stepContext.BeginDialogAsync(nameof(ExamDialog), null, cancellationToken),
                2 => await stepContext.BeginDialogAsync(nameof(ScheduleDialog), null, cancellationToken),
                _ => await stepContext.EndDialogAsync(null, cancellationToken)
            };
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.ReplaceDialogAsync(stepContext.ActiveDialog.Id, stepContext.Options, cancellationToken);
        }
    }

}
