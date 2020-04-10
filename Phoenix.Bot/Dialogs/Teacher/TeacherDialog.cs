using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs.Student
{
    public class TeacherDialog : ComponentDialog
    {
        public TeacherDialog()
            : base(nameof(TeacherDialog))
        {
            AddDialog(new ExerciseDialog());
            //AddDialog(new ExamsDialog());
            //AddDialog(new GradationDialog());
            //AddDialog(new ScheduleDialog());
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new WaterfallDialog(nameof(TeacherDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    MenuStepAsync,
                    TaskStepAsync,
                    LoopStepAsync
                }));

            InitialDialogId = nameof(TeacherDialog) + "_" + nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> MenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(
                nameof(ChoicePrompt),
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
            return (stepContext.Result as FoundChoice).Index switch
            {
                0 => await stepContext.BeginDialogAsync(nameof(ExerciseDialog), null, cancellationToken),
                //1 => await stepContext.BeginDialogAsync(nameof(ExamsDialog), null, cancellationToken),
                //2 => await stepContext.BeginDialogAsync(nameof(GradationDialog), null, cancellationToken),
                //3 => await stepContext.BeginDialogAsync(nameof(ScheduleDialog), null, cancellationToken),
                _ => new DialogTurnResult(DialogTurnStatus.Cancelled)
            };
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var options = new Dictionary<string, object> { { "IsGreeted", true } };
            return await stepContext.ReplaceDialogAsync(nameof(TeacherDialog) + "_" + nameof(WaterfallDialog), options, cancellationToken);
        }
    }
}
