using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Phoenix.Bot.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs.Student
{
    public class ExerciseDialog : ComponentDialog
    {
        public ExerciseDialog() :
            base(nameof(ExerciseDialog))
        {
            AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));
            AddDialog(new WaterfallDialog(nameof(ExerciseDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    CourseSelectStepAsync,
                    NextOrOldCourseSelectStepAsync,
                    FinalStepAsync  
                }));

            InitialDialogId = nameof(ExerciseDialog) + "_" + nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> CourseSelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Επίλεξε το μάθημα που σε ενδιαφέρει:"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ή πληκτρολόγησε μία από τις παρακάτω απαντήσεις:"),
                    Choices = ChoiceFactory.ToChoices(new string[] { "Αγγλικά", "Γαλλικά" })
                });
        }

        private async Task<DialogTurnResult> NextOrOldCourseSelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var course = (stepContext.Result as FoundChoice).Value;
            var nextDate = "27/1/2020";
            var link = $"https://nuage.azurewebsites.net/extensions/student/homework?course={course}";

            var card = new HeroCard 
            {
                Text = "Θα ήθελες να δεις τις εργασίες για το επόμενο μάθημα ή για κάποιο παλαιότερο;",
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.OpenUrl, title: "Για το επόμενο", value: link + $"&seq=next&date={nextDate}"),
                    new CardAction(ActionTypes.OpenUrl, title: "Για προηγούμενο", value: link + "&seq=prev")
                }
            };

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = (Activity)MessageFactory.Attachment(card.ToAttachment()),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ή πληκτρολόγησε μία από τις παρακάτω απαντήσεις:"),
                    Choices = ChoiceFactory.ToChoices(new string[] { "Αρχικό μενού", "Άλλο μάθημα" })
                });
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is FoundChoice)
            {
                return (stepContext.Result as FoundChoice).Index switch
                {
                    0 => await stepContext.EndDialogAsync(null, cancellationToken),
                    1 => await stepContext.ReplaceDialogAsync(nameof(ExerciseDialog) + "_" + nameof(WaterfallDialog)),
                    _ => new DialogTurnResult(DialogTurnStatus.Cancelled)
                };
            }

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
    }
}
