using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Phoenix.Bot.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs.Student.Common
{
    public class CourseDialog : ComponentDialog
    {
        private static class WaterfallNames
        {
            public const string Course = "Student_CommonCourse_WaterfallDialog";
        }

        public CourseDialog() :
            base(nameof(CourseDialog))
        {
            AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));

            AddDialog(new WaterfallDialog(WaterfallNames.Course,
                new WaterfallStep[]
                {
                    CourseStepAsync,
                    CourseSelectStepAsync
                }));

            InitialDialogId = WaterfallNames.Course;
        }

        #region Course Waterfall Dialog

        private async Task<DialogTurnResult> CourseStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var coursesIdName = stepContext.Options as Tuple<int, string>[];

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Για ποιο μάθημα θα ήθελες να δεις τις εργασίες σου;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ή πληκτρολόγησε ένα από τα παρακάτω μαθήματα:"),
                    Choices = ChoiceFactory.ToChoices(coursesIdName.Select(t => t.Item2).ToList())
                });
        }

        private async Task<DialogTurnResult> CourseSelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var selCourseIndex = (stepContext.Result as FoundChoice).Index;
            var coursesIdName = stepContext.Options as Tuple<int, string>[];

            return await stepContext.EndDialogAsync(coursesIdName[selCourseIndex].Item1, cancellationToken);
        }

        #endregion
    }
}
