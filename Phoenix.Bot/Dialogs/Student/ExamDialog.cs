using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Phoenix.Bot.Dialogs.Student.Common;
using Phoenix.Bot.Extensions;
using Phoenix.Bot.Helpers;
using Phoenix.DataHandle.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Phoenix.Bot.Helpers.CardHelper;

namespace Phoenix.Bot.Dialogs.Student
{
    public class ExamDialog : ComponentDialog
    {
        private readonly PhoenixContext _phoenixContext;
        private readonly BotState _conversationState;

        private readonly IStatePropertyAccessor<int> _selCourseId;

        private static class WaterfallNames
        {
            public const string Main        = "StudentExam_Main_WaterfallDialog";
            public const string FutureExam  = "StudentExam_FutureExam_WaterfallDialog";
            public const string Material    = "StudentExam_Material_WaterfallDialog";
            public const string PastExam    = "StudentExam_PastExam_WaterfallDialog";
            public const string Grade       = "StudentExam_Grade_WaterfallDialog";
        }

        public ExamDialog(PhoenixContext phoenixContext, ConversationState conversationState)
            : base(nameof(ExamDialog))
        {
            _phoenixContext = phoenixContext;
            _conversationState = conversationState;

            _selCourseId = _conversationState.CreateProperty<int>("SelCourseId");

            AddDialog(new CourseDialog());
            AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));
            AddDialog(new DateTimePrompt(nameof(DateTimePrompt), null, "fr-fr"));

            AddDialog(new WaterfallDialog(WaterfallNames.Main,
                new WaterfallStep[]
                {
                    CourseStepAsync,
                    ExamStepAsync,
                    RedirectStepAsync,
                    OtherStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.FutureExam,
                new WaterfallStep[]
                {
                    FollowingExamStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Material,
                new WaterfallStep[]
                {
                    MaterialStepAsync,
                    MaterialPageStepAsync,
                    MaterialOtherStepAsync,
                    MaterialOtherSelectStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.PastExam,
                new WaterfallStep[]
                {
                    //PreviousExamStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Grade,
                new WaterfallStep[]
                {
                    //GradeStepAsync,
                    //GradePageStepAsync,
                    //GradeOtherStepAsync,
                    //GradeOtherSelectStepAsync
                }));

            InitialDialogId = WaterfallNames.Main;
        }

        protected override Task OnEndDialogAsync(ITurnContext context, DialogInstance instance, DialogReason reason, CancellationToken cancellationToken = default)
        {
            Task.Run(async () => await _selCourseId.DeleteAsync(context));

            return base.OnEndDialogAsync(context, instance, reason, cancellationToken);
        }

        #region Main Waterfall Dialog

        private async Task<DialogTurnResult> CourseStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string FbId = stepContext.Context.Activity.From.Id;
            var coursesIdName = _phoenixContext.Course.
                Where(c => c.StudentCourse.Any(sc => sc.CourseId == c.Id && sc.Student.AspNetUser.FacebookId == FbId)).
                Select(c => Tuple.Create(c.Id, c.Name)).
                ToArray();

            if (coursesIdName.Length == 0)
            {
                await stepContext.Context.SendActivityAsync("Απ' ό,τι φαίνεται δεν έχεις εγγραφεί σε κάποιο μάθημα προς το παρόν.");
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }

            if (coursesIdName.Length == 1)
                return await stepContext.NextAsync(coursesIdName[0].Item1, cancellationToken);

            return await stepContext.BeginDialogAsync(nameof(CourseDialog), coursesIdName, cancellationToken);
        }

        private async Task<DialogTurnResult> ExamStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            int selCourseId = Convert.ToInt32(stepContext.Result);
            await _selCourseId.SetAsync(stepContext.Context, selCourseId);
            string fbId = stepContext.Context.Activity.From.Id;
            var exams = _phoenixContext.Exam.Where(e => e.CourseId == selCourseId);

            bool anyExams = exams.Any();
            bool anyFutureExams = exams.Any(e => e.EndsAt >= DialogHelper.GreeceLocalTime());
            bool anyGradedExams = exams.Any(e => e.StudentExam.Any(se => se.Student.AspNetUser.FacebookId == fbId && se.Exam.Id == e.Id && se.Grade != null));

            if (!anyExams)
            {
                await stepContext.Context.SendActivityAsync("Δεν υπάρχουν ακόμα διαγωνίσματα για αυτό το μάθημα.");
                await stepContext.Context.SendActivityAsync("Απόλαυσε τον ελέυθερο χρόνο σου! 😎");

                return await stepContext.EndDialogAsync(null, cancellationToken);
            }

            if (!anyFutureExams && !anyGradedExams)
            {
                await stepContext.Context.SendActivityAsync("Δεν υπάρχουν προγραμματισμένα διαγωνίσματα, ούτε έχουν βγει βαθμοί για αυτό το μάθημα.");

                return await stepContext.EndDialogAsync(null, cancellationToken);
            }

            if (!anyGradedExams)
                return await stepContext.NextAsync(0, cancellationToken);
            if (!anyFutureExams)
                return await stepContext.NextAsync(1, cancellationToken);

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Θα ήθελες να δεις την ύλη για επόμενα διαγωνίσματα ή τους βαθμούς για παλαιότερα;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ή πληκρολόγησε μία από τις παρακάτω επιλογές:"),
                    Choices = new Choice[] { new Choice("Ύλη"), new Choice("Βαθμοί") }
                });
        }

        private async Task<DialogTurnResult> RedirectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            int choice = stepContext.Result is int res ? res : (stepContext.Result as FoundChoice).Index;

            return choice == 0 ? 
               await stepContext.BeginDialogAsync(WaterfallNames.FutureExam, null, cancellationToken) :
               await stepContext.BeginDialogAsync(WaterfallNames.PastExam, null, cancellationToken);
        }

        private async Task<DialogTurnResult> OtherStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is FoundChoice foundChoice && foundChoice.Index == 0)
                return await stepContext.ReplaceDialogAsync(WaterfallNames.Main, null, cancellationToken);

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        #endregion

        #region Future Exam Waterfall Dialog

        private async Task<DialogTurnResult> FollowingExamStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            int selCourseId = await _selCourseId.GetAsync(stepContext.Context);

            var nextExam = _phoenixContext.Exam.
                Include(e => e.Classroom).
                Where(e => e.CourseId == selCourseId && e.EndsAt >= DialogHelper.GreeceLocalTime()).
                ToList().
                Aggregate((e, ne) => e.StartsAt < ne.StartsAt ? e : ne);

            await stepContext.Context.SendActivityAsync($"Το αμέσως επόμενο διαγώνισμα είναι την {nextExam.StartsAt:dddd} " +
                $"{nextExam.StartsAt:m} στις {nextExam.StartsAt:t}" + (nextExam.ClassroomId != null ? $" στην αίθουσα {nextExam.Classroom.Name}." : "."));

            return await stepContext.ReplaceDialogAsync(WaterfallNames.Material, nextExam.Id, cancellationToken);
        }

        #endregion

        #region Material Waterfall Dialog

        //Shows Material only for upcoming exams
        private async Task<DialogTurnResult> MaterialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            int examId = Convert.ToInt32(stepContext.Options);
            var examDate = (await _phoenixContext.Exam.FindAsync(examId)).StartsAt;
            var courseName = (await _phoenixContext.Course.FindAsync(await _selCourseId.GetAsync(stepContext.Context))).Name;

            var pageAcsr = _conversationState.CreateProperty<int>("MaterialPage");
            int page = await pageAcsr.GetAsync(stepContext.Context);
            const int pageSize = 3;

            var paginatedMat = _phoenixContext.Material.
                Include(m => m.Book).
                ToList().
                Where(m => m.ExamId == examId).
                Where((_, i) => i >= pageSize * page && i < pageSize * (page + 1));

            int matShownCount = page * pageSize;
            foreach (var mat in paginatedMat)
            {
                await stepContext.Context.SendActivityAsync(new Activity(type: ActivityTypes.Typing));

                var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 2));
                card.BackgroundImage = new AdaptiveBackgroundImage("https://www.bot.askphoenix.gr/assets/4f5d75_sq.png");
                card.Body.Add(new AdaptiveTextBlockHeaderLight($"Ύλη {++matShownCount} - {courseName} - {examDate:m}"));
                card.Body.Add(new AdaptiveRichFactSetLight("Βιβλίο ", mat.Book.Name));
                card.Body.Add(new AdaptiveRichFactSetLight("Κεφάλαιο ", mat.Chapter, separator: true));
                card.Body.Add(new AdaptiveRichFactSetLight("Ενότητα ", mat.Section, separator: true));
                card.Body.Add(new AdaptiveRichFactSetLight("Σχόλια ", string.IsNullOrEmpty(mat.Comments) ? "-" : mat.Comments, separator: true));

                await stepContext.Context.SendActivityAsync(
                    MessageFactory.Attachment(new Attachment(contentType: AdaptiveCard.ContentType, content: JObject.FromObject(card))));
            }

            int matCount = _phoenixContext.Material.Count(m => m.ExamId == examId);
            if (matShownCount < matCount)
            {
                int matLeft = matCount - matShownCount;
                int showMoreNum = matLeft <= pageSize ? matLeft : pageSize;
                bool singular = matLeft == 1;

                await pageAcsr.SetAsync(stepContext.Context, page + 1);

                return await stepContext.PromptAsync(
                    nameof(UnaccentedChoicePrompt),
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text($"Υπάρχ{(singular ? "ει" : "ουν")} ακόμη {matLeft} σημεί{(singular ? "ο" : "α")} " +
                            "που πρέπει να διαβάσεις."),
                        RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε μία από τις παρακάτω απαντήσεις:"),
                        Choices = new Choice[] { new Choice($"Εμφάνιση {showMoreNum} ακόμη"), new Choice("Ολοκλήρωση") }
                    });
            }

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> MaterialPageStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is FoundChoice foundChoice && foundChoice.Index == 0)
                return await stepContext.ReplaceDialogAsync(WaterfallNames.Material, stepContext.Options, cancellationToken);

            await _conversationState.CreateProperty<int>("MaterialPage").DeleteAsync(stepContext.Context);

            int selCourseId = await _selCourseId.GetAsync(stepContext.Context);
            string fbId = stepContext.Context.Activity.From.Id;

            if (_phoenixContext.Exam.Count(e => e.CourseId == selCourseId) == 1)
            {
                await stepContext.Context.SendActivityAsync("Αυτό ήταν το μόνο διαγώνισμα που έχεις.");
                
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
            if (_phoenixContext.Exam.Count(e => e.CourseId == selCourseId && e.EndsAt >= DialogHelper.GreeceLocalTime()) == 1)
            {
                await stepContext.Context.SendActivityAsync("Αυτό ήταν το μόνο προγραμματισμένο διαγώνισμα.");

                return await stepContext.EndDialogAsync(true, cancellationToken);
            }

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Θα ήθελες να δεις την ύλη για άλλο διαγώνισμα;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ απάντησε με ένα Ναι ή Όχι:"),
                    Choices = new Choice[] { new Choice("Ναι"), new Choice("Όχι, ευχαριστώ") { Synonyms = new List<string> { "Όχι" } } }
                });
        }

        private async Task<DialogTurnResult> MaterialOtherStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((stepContext.Result as FoundChoice).Index == 1)
                return await stepContext.EndDialogAsync(null, cancellationToken);

            var selCourseId = await _selCourseId.GetAsync(stepContext.Context);

            var examDates = _phoenixContext.Exam.
                Where(e => e.CourseId == selCourseId && e.EndsAt >= DialogHelper.GreeceLocalTime()).
                Select(e => e.StartsAt).
                OrderByDescending(d => d).
                Take(5).
                Select(d => $"{d.Day}/{d.Month}").
                ToList();

            var prompt = ChoiceFactory.SuggestedAction(ChoiceFactory.ToChoices(examDates),
                text: "Επίλεξε μία από τις παρακάτω ημερομηνίες ή γράψε μια άλλη:");
            var reprompt = ChoiceFactory.SuggestedAction(ChoiceFactory.ToChoices(examDates),
                text: "Η επιθυμητή ημερομηνία θα πρέπει να είναι στη μορφή ημέρα/μήνας (π.χ. 24/4)):");

            return await stepContext.PromptAsync(
                nameof(DateTimePrompt),
                new PromptOptions
                {
                    Prompt = (Activity)prompt,
                    RetryPrompt = (Activity)reprompt
                });
        }

        private async Task<DialogTurnResult> MaterialOtherSelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var selCourseId = await _selCourseId.GetAsync(stepContext.Context);

            var selDate = DialogHelper.ResolveDateTime(stepContext.Result as IList<DateTimeResolution>);
            var exam = _phoenixContext.Exam.
                Include(e => e.Classroom).
                FirstOrDefault(e => e.CourseId == selCourseId && e.StartsAt.Date == selDate.Date);

            if (exam == null)
            {
                exam = _phoenixContext.Exam.
                    Include(e => e.Classroom).
                    Where(e => e.CourseId == selCourseId && e.EndsAt > DialogHelper.GreeceLocalTime()).
                    ToList().
                    Aggregate((e, fe) => Math.Abs((e.StartsAt - selDate).Days) < Math.Abs((fe.StartsAt - selDate).Days) ? e : fe);

                await stepContext.Context.SendActivityAsync($"Δεν υπάρχει διαγώνισμα για αυτό το μάθημα στις {selDate:m}.");
                await stepContext.Context.SendActivityAsync($"Βρήκα όμως το πιο κοντινό του στις {exam.StartsAt:m}:");

                return await stepContext.ReplaceDialogAsync(WaterfallNames.Material, exam.Id, cancellationToken);
            }

            string dayArticle = DialogHelper.GreekDayArticle(exam.StartsAt.DayOfWeek);
            await stepContext.Context.SendActivityAsync($"Για το διαγώνισμα {dayArticle} {exam.StartsAt.DayOfWeek} " +
                    $"{exam.StartsAt:m} στις {exam.StartsAt:t}" 
                    + exam.ClassroomId != null ? $" στην αίθουσα {exam.Classroom.Name} " : " "
                    + "έχεις να διαβάσεις τα παρακάτω:");
            return await stepContext.ReplaceDialogAsync(WaterfallNames.Material, exam.Id, cancellationToken);
        }

        #endregion
    }
}
