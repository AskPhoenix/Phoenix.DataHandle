using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Phoenix.Bot.Dialogs.Student;
using Phoenix.Bot.Helpers;

namespace Phoenix.Bot.Dialogs.Teacher
{
    public class TeacherDialog : ComponentDialog
    {
        public TeacherDialog()
            : base(nameof(TeacherDialog))
        {
            this.AddDialog(new ExerciseDialog());
            //AddDialog(new ExamDialog());
            //AddDialog(new GradationDialog());
            //AddDialog(new ScheduleDialog());
            this.AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));
            this.AddDialog(new WaterfallDialog(nameof(TeacherDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    this.MenuStepAsync,
                    this.TaskStepAsync,
                    this.LoopStepAsync
                }));

            this.InitialDialogId = nameof(TeacherDialog) + "_" + nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> MenuStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
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
            return (stepContext.Result as FoundChoice).Index switch
            {
                0 => await stepContext.BeginDialogAsync(nameof(ExerciseDialog), null, cancellationToken),
                //1 => await stepContext.BeginDialogAsync(nameof(ExamDialog), null, cancellationToken),
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
