using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Newtonsoft.Json.Linq;
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
    public class ExerciseDialog : ComponentDialog
    {
        private readonly PhoenixContext _phoenixContext;

        private Course[] Courses { get; set; }
        private int SelCourse { get; set; }

        private static class WaterfallNames
        {
            public const string Course          = "StudentExercise_Course_WaterfallDialog";
            public const string Lecture         = "StudentExercise_Lecture_WaterfallDialog";
            public const string LecturePast     = "StudentExercise_LecturePast_WaterfallDialog";
            public const string LectureOther    = "StudentExercise_LectureOther_WaterfallDialog";
            public const string Homework        = "StudentExercise_Homework_WaterfallDialog";
        }

        public ExerciseDialog(PhoenixContext phoenixContext) :
            base(nameof(ExerciseDialog))
        {
            _phoenixContext = phoenixContext;

            AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));
            AddDialog(new DateTimePrompt(nameof(DateTimePrompt), null, "el-gr"));

            AddDialog(new WaterfallDialog(WaterfallNames.Course,
                new WaterfallStep[]
                {
                    CourseStepAsync,
                    CourseSelectStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Lecture,
                new WaterfallStep[]
                {
                    LectureStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.LecturePast,
                new WaterfallStep[]
                {
                    PastLectureAskStepAsync,
                    PastLectureStepAsync,
                    PastLectureSelectStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.LectureOther,
                new WaterfallStep[]
                {
                    OtherLectureStepAsync,
                    OtherLectureSelectStepAsync,
                    OtherLectureNotFoundStepAsync,
                    OtherLectureAgainStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Homework,
                new WaterfallStep[]
                {
                    HomeworkStepAsync
                }));
        }

        protected override async Task<DialogTurnResult> OnBeginDialogAsync(DialogContext innerDc, object options, CancellationToken cancellationToken = default)
        {
            string FbId = innerDc.Context.Activity.From.Id;
            Courses = _phoenixContext.StudentCourse.Where(sc => sc.Student.AspNetUser.FacebookId == FbId).Select(sc => sc.Course).ToArray();
            
            if (Courses == null || Courses.Length == 0)
            {
                await innerDc.Context.SendActivityAsync("Απ' ό,τι φαίνεται δεν έχεις εγγραφεί σε κάποιο μάθημα προς το παρόν.");
                return await innerDc.EndDialogAsync(null, cancellationToken);
            }

            InitialDialogId = Courses.Length == 1 ? InitialDialogId = WaterfallNames.Lecture : WaterfallNames.Course;

            return await base.OnBeginDialogAsync(innerDc, options, cancellationToken);
        }

        #region Course Waterfall Dialog

        private async Task<DialogTurnResult> CourseStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Για ποιο μάθημα θα ήθελες να δεις τις εργασίες σου;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ή πληκτρολόγησε ένα από τα παρακάτω μαθήματα:"),
                    Choices = ChoiceFactory.ToChoices(Courses.Select(c => c.Name).ToList())
                });

        private async Task<DialogTurnResult> CourseSelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.BeginDialogAsync(WaterfallNames.Lecture, (stepContext.Result as FoundChoice)?.Index, cancellationToken);

        #endregion

        #region Lecture Waterfall Dialog

        private async Task<DialogTurnResult> LectureStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            this.SelCourse = stepContext.Options is int ? (int)stepContext.Options : 0;

            if (!_phoenixContext.Lecture.Any(l => l.CourseId == Courses[SelCourse].Id && l.Homework.Count > 0))
            {
                await stepContext.Context.SendActivityAsync("Δεν υπάρχουν ακόμα εργασίες για αυτό το μάθημα.");
                await stepContext.Context.SendActivityAsync("Απόλαυσε τον ελέυθερο χρόνο σου! 😎");

                return await stepContext.EndDialogAsync(null, cancellationToken);
            }

            var grNow = DialogHelper.GreeceLocalTime();
            if (_phoenixContext.Lecture.Any(l => l.CourseId == Courses[SelCourse].Id && l.StartDateTime >= grNow && l.Homework.Count > 0))
            {
                var nLec = _phoenixContext.Lecture.
                    Where(l => l.CourseId == Courses[SelCourse].Id && l.StartDateTime >= grNow && l.Homework.Count > 0).
                    Aggregate((l, nl) => l.StartDateTime < nl.StartDateTime ? l : nl);

                await stepContext.Context.SendActivityAsync("Οι εργασίες με την κοντινότερη προθεσμία " +
                    $"είναι για τις {nLec.StartDateTime:m} και είναι οι παρακάτω:");

                return await stepContext.BeginDialogAsync(WaterfallNames.Homework, nLec.Homework, cancellationToken);
            }

            return await stepContext.BeginDialogAsync(WaterfallNames.LecturePast, null, cancellationToken);
        }

        #endregion

        #region Lecture Past Waterfall Dialog

        private async Task<DialogTurnResult> PastLectureAskStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync("Δεν έχεις νέες εργασίες για αυτό το μάθημα!");

            return await stepContext.PromptAsync(
            nameof(UnaccentedChoicePrompt),
            new PromptOptions
            {
                Prompt = MessageFactory.Text("Θα ήθελες να δεις παλαιότερες εργασίες σου;"),
                RetryPrompt = MessageFactory.Text("Παρακαλώ απάντησε με ένα Ναι ή Όχι:"),
                Choices = new Choice[] { new Choice("Ναι"), new Choice("Όχι, ευχαριστώ") { Synonyms = new List<string> { "Όχι" } } }
            });
        }

        private async Task<DialogTurnResult> PastLectureStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((stepContext.Result as FoundChoice).Index == 1)
            {
                await stepContext.Context.SendActivityAsync("Εντάξει! Όποτε θέλεις μπορείς να ελέγξεις ξανά για νέες εργασίες! 😊");
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }

            var grNow = DialogHelper.GreeceLocalTime();

            var pLecDates = _phoenixContext.Lecture.
                Where(l => l.CourseId == Courses[SelCourse].Id && l.StartDateTime < grNow && l.Homework.Count > 0).
                OrderByDescending(h => h.StartDateTime).
                Take(5).
                Select(l => $"{l.StartDateTime.Day}/{l.StartDateTime.Month}").
                ToArray();

            await stepContext.Context.SendActivityAsync("Ωραία! Παρακάτω θα βρεις μερικές από τις πιο πρόσφατες ημερομηνίες που είχες μάθημα.");

            //TODO: Check if DateTimePrompt can have choices
            return await stepContext.PromptAsync(
            nameof(DateTimePrompt),
            new PromptOptions
            {
                Prompt = MessageFactory.Text("Επίλεξε μία από αυτές ή πληκτρολόγησε κάποια άλλη παρακάτω:"),
                RetryPrompt = MessageFactory.Text("Η επιθυμητή ημερομηνία θα πρέπει να είναι στη μορφή ημέρα/μήνας (π.χ. 24/4)):"),
                Choices = ChoiceFactory.ToChoices(pLecDates)
            });
        }

        private async Task<DialogTurnResult> PastLectureSelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.ReplaceDialogAsync(WaterfallNames.LectureOther, stepContext.Result, cancellationToken);

        #endregion

        #region Homework Waterfall Dialog

        private async Task<DialogTurnResult> HomeworkStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var hw = stepContext.Options as ICollection<Homework>;

            if (!stepContext.Values.ContainsKey("hwPaginationInd"))
                stepContext.Values.Add("hwPaginationInd", 0);
            int page = (int)stepContext.Values["hwPaginationInd"];

            string fbId = stepContext.Context.Activity.From.Id;
            bool forPastLec = hw.First().ForLecture.StartDateTime < DialogHelper.GreeceLocalTime();
            decimal? grade = null;
            const int pageSize = 5;
            List<Attachment> hwCards = new List<Attachment>(pageSize);

            foreach (var ex in hw.Where((h, i) => i >= pageSize * page && i < pageSize * (page + 1)).Select(h => h.Exercise))
            {
                var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 2));
                card.BackgroundImage = new AdaptiveBackgroundImage("https://www.bot.askphoenix.gr/assets/4f5d75_sq.png");
                card.Body.Add(new AdaptiveTextBlockHeaderLight(ex.Name));
                card.Body.Add(new AdaptiveRichFactSetLight("Βιβλίο", ex.Book.Name));
                card.Body.Add(new AdaptiveRichFactSetLight("Σελίδα", ex.Page.ToString(), separator: true));
                if (forPastLec)
                {
                    grade = ex.StudentExercise.Single(se => se.ExerciseId == ex.Id && se.Student.AspNetUser.FacebookId == fbId).Grade;
                    card.Body.Add(new AdaptiveRichFactSetLight("Βαθμός", grade.ToString() ?? "-", separator: true));
                }
                card.Body.Add(new AdaptiveRichFactSetLight("Σχόλια", string.IsNullOrEmpty(ex.Info) ? "-" : ex.Info, separator: true));

                hwCards.Add(new Attachment(contentType: AdaptiveCard.ContentType, content: JObject.FromObject(card)));
            }

            await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(hwCards));

            return await stepContext.PromptAsync(
            nameof(UnaccentedChoicePrompt),
            new PromptOptions
            {
                Prompt = MessageFactory.Text("Θα ήθελες να δεις τις εργασίες που έχεις για άλλη ημερομηνία;"),
                RetryPrompt = MessageFactory.Text("Παρακαλώ απάντησε με ένα Ναι ή Όχι:"),
                Choices = new Choice[] { new Choice("Ναι"), new Choice("Όχι, ευχαριστώ") { Synonyms = new List<string> { "Όχι" } } }
            });
        }

        #endregion

        #region Lecture Other Waterfall Dialog

        private async Task<DialogTurnResult> OtherLectureStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Options is DateTimeResolution[])
                return await stepContext.NextAsync(stepContext.Options, cancellationToken);

            var foundChoice = (stepContext.Result ?? stepContext.Options) as FoundChoice;
            if (foundChoice.Index == 1)
            {
                await stepContext.Context.SendActivityAsync("ΟΚ! 😊");
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }

            //TODO: Check if DateTimePrompt can have choices
            return await stepContext.PromptAsync(
                nameof(DateTimePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Γράψε παρακάτω την ημερομηνία στη μορφή η/μ:"),
                    RetryPrompt = MessageFactory.Text("Η επιθυμητή ημερομηνία θα πρέπει να είναι στη μορφή ημέρα/μήνας (π.χ. 24/4)):")
                });
        }

        private async Task<DialogTurnResult> OtherLectureSelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var selDate = DateTime.Parse((stepContext.Result as DateTimeResolution[]).First().Value);
            var lec = _phoenixContext.Lecture.FirstOrDefault(l => l.CourseId == Courses[SelCourse].Id && l.StartDateTime.Date == selDate.Date);

            if (lec == null)
            {
                lec = _phoenixContext.Lecture.
                    Where(l => l.CourseId == Courses[SelCourse].Id && l.Homework.Count > 0).
                    DefaultIfEmpty().
                    Aggregate((l, cl) => Math.Abs((l.StartDateTime - selDate).Days) < Math.Abs((cl.StartDateTime - selDate).Days) ? l : cl);

                if (lec == null)
                {
                    await stepContext.Context.SendActivityAsync($"Δεν υπάρχει διάλεξη για αυτό το μάθημα στις {selDate:m}.");
                    await stepContext.Context.SendActivityAsync("Έψαξα και 5 ημέρες πριν ή μετά, αλλά δε βρήκα κάτι.");
                    return await stepContext.NextAsync(null, cancellationToken);
                }

                await stepContext.Context.SendActivityAsync($"Δεν υπάρχει διάλεξη για αυτό το μάθημα στις {selDate:m}.");
                await stepContext.Context.SendActivityAsync($"Βρήκα όμως τις εργασίες για τις {lec.StartDateTime:m}:");
                return await stepContext.ReplaceDialogAsync(WaterfallNames.Homework, lec.Homework, cancellationToken);
            }

            if (lec.Homework.Count == 0)
            {
                await stepContext.Context.SendActivityAsync($"Δεν υπάρχουν εργασίες για τις {selDate:m}");
                return await stepContext.NextAsync(null, cancellationToken);
            }

            await stepContext.Context.SendActivityAsync($"Για τις {selDate:m} έχεις τις παρακάτω εργασίες:");
            return await stepContext.ReplaceDialogAsync(WaterfallNames.Homework, lec.Homework, cancellationToken);
        }

        private async Task<DialogTurnResult> OtherLectureNotFoundStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Θα ήθελες να δοκιμάσεις ξανά με άλλη ημερομηνία;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ απάντησε με ένα Ναι ή Όχι:"),
                    Choices = new Choice[] { new Choice("Ναι"), new Choice("Όχι, ευχαριστώ") { Synonyms = new List<string> { "Όχι" } } }
                });

        private async Task<DialogTurnResult> OtherLectureAgainStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.ReplaceDialogAsync(WaterfallNames.LectureOther, stepContext.Result, cancellationToken);

        #endregion
    }
}
