using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs.Student
{
    public class StudentDialog : ComponentDialog
    {
        private static readonly string WaterfallDialogId = nameof(StudentDialog) + "_" + nameof(WaterfallDialog);

        public StudentDialog()
            : base(nameof(StudentDialog))
        {
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ExerciseDialog());
            AddDialog(new ExamsDialog());
            AddDialog(new ScheduleDialog());
            AddDialog(new WaterfallDialog(WaterfallDialogId,
                new WaterfallStep[]
                {
                    MenuStepAsync,
                    TaskStepAsync,
                    LoopStepAsync
                }));

            InitialDialogId = WaterfallDialogId;
        }

        private async Task<DialogTurnResult> MenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var options = stepContext.Options as Dictionary<string, object>;
            string messText = "Πώς θα μπορούσα να σε βοηθήσω;";
            //string messText = options?.ContainsKey("IsGreeted") ?? false ? "Θα ήθελες κάποια άλλη πληροφορία;" : "Πώς θα μπορούσα να σε βοηθήσω;";

            return await stepContext.PromptAsync(
                nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text(messText),
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
                1 => await stepContext.BeginDialogAsync(nameof(ExamsDialog), null, cancellationToken),
                2 => await stepContext.BeginDialogAsync(nameof(ScheduleDialog), null, cancellationToken),
                _ => new DialogTurnResult(DialogTurnStatus.Cancelled)
            };
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var options = new Dictionary<string, object> { { "IsGreeted", true } };
            return await stepContext.ReplaceDialogAsync(WaterfallDialogId, options, cancellationToken);
        }
    }

}
