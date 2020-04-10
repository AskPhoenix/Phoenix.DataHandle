using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs.Student
{
    public class ExamsDialog : ComponentDialog
    {
        public ExamsDialog()
            : base(nameof(ExamsDialog))
        {
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt), DialogExtensions.UseExtraValidations));
            AddDialog(new WaterfallDialog(nameof(ExamsDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    CourseSelectStepAsync,
                    LastExamsStepAsync,
                    NextExamsContentStepAsync,
                    FinalStepAsync
                }));

            InitialDialogId = nameof(ExamsDialog) + "_" + nameof(WaterfallDialog);
        }

        private string[] DummyCourses = { "Αγγλικά", "Γαλλικά" };

        private async Task<DialogTurnResult> CourseSelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Options is Dictionary<string, int> && (stepContext.Options as Dictionary<string, int>).TryGetValue("Course", out int courseId))
                return await stepContext.NextAsync(courseId, cancellationToken);

            return await stepContext.PromptAsync(
                nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Επίλεξε το μάθημα που σε ενδιαφέρει:"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ή πληκτρολόγησε μία από τις παρακάτω απαντήσεις:"),
                    Choices = ChoiceFactory.ToChoices(DummyCourses)
                });
        }

        private async Task<DialogTurnResult> LastExamsStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var courseId = stepContext.Result is int ? (int)stepContext.Result : (stepContext.Result as FoundChoice).Index;
            stepContext.Values.Add("Course", courseId);
            var lastDate = "27/1/2020";

            var card = new HeroCard
            {
                Title = $"Διαγωνίσματα στα {DummyCourses[courseId]}",
                Text = $"Στο τελευταίο διαγώνισμα στις {lastDate} πήρες 20! Συνέχισε έτσι! :D",
                Tap = new CardAction(ActionTypes.OpenUrl,
                    value: $"https://nuage.azurewebsites.net/extensions/student/exams?course={courseId}"),
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.ImBack, title: "Ύλη για το επόμενο", value: "Ύλη"),
                    new CardAction(ActionTypes.ImBack, title: "Ιστορικό", value: "Ιστορικό")
                }
            };

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = (Activity)MessageFactory.Attachment(card.ToAttachment()),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ χρησιμοποίησε τα κουμπιά στην προηγούμενη κάρτα ή μία από τις παρακάτω επιλογές:"),
                    Choices = ChoiceFactory.ToChoices(new string[] { "Αρχικό μενού", "Άλλο μάθημα" }),
                    Validations = new string[] { "Ύλη", "Ιστορικό" }
                });
        }


        private async Task<DialogTurnResult> NextExamsContentStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is FoundChoice)
            {
                return (stepContext.Result as FoundChoice).Index switch
                {
                    0 => await stepContext.EndDialogAsync(null, cancellationToken),
                    1 => await stepContext.ReplaceDialogAsync(nameof(ExamsDialog) + "_" + nameof(WaterfallDialog)),
                    _ => new DialogTurnResult(DialogTurnStatus.Cancelled)
                };
            }
            else if (stepContext.Context.Activity.Text == "Ύλη")
            {
                var reply = MessageFactory.Text("Το επόμενο διαγώνισμα είναι προγραμματισμένο για τις 01/03/2020.");
                await stepContext.Context.SendActivityAsync(reply);

                var cards = new List<Attachment>(2);
                for (int i = 1; i <= 2; i++)
                {
                    cards.Add( new HeroCard
                    {
                        Title = i == 1 ? "Student's Book" : "Companion Book",
                        Text = i == 1 ? "- Pages 27-30" : "- Chapters 8 - 9",
                        Tap = new CardAction(ActionTypes.OpenUrl,
                            value: $"https://nuage.azurewebsites.net/extensions/student/exams?course={stepContext.Values["Course"]}&req=content")
                    }.ToAttachment());
                }
                
                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                    new PromptOptions
                    {
                        Prompt = (Activity)MessageFactory.Carousel(cards),
                        RetryPrompt = MessageFactory.Text("Παρακαλώ χρησιμοποίησε μία από τις παρακάτω επιλογές:"),
                        Choices = ChoiceFactory.ToChoices(new string[] { "Αρχικό μενού", "Άλλο μάθημα", "Πίσω" })
                    });
            }
            else if (stepContext.Context.Activity.Text == "Ιστορικό")
            {
                //TODO: Pagination
                var cards = new List<Attachment>(3);
                for (int i = 1; i <= 3; i++)
                {
                    int grade = new Random().Next(10, 21);

                    cards.Add(new HeroCard
                    {
                        Title = $"Διαγώνισμα {i}ο",
                        Text = grade.ToString() + " " + (grade >= 18 ? "Άριστα!" : grade >= 15 ? "Πολύ καλά!" : "Καλά!"),
                        Tap = new CardAction(ActionTypes.OpenUrl,
                            value: $"https://nuage.azurewebsites.net/extensions/student/exams?course={stepContext.Values["Course"]}&req=history")
                    }.ToAttachment());
                }

                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                    new PromptOptions
                    {
                        Prompt = (Activity)MessageFactory.Carousel(cards),
                        RetryPrompt = MessageFactory.Text("Παρακαλώ χρησιμοποίησε μία από τις παρακάτω επιλογές:"),
                        Choices = ChoiceFactory.ToChoices(new string[] { "Αρχικό μενού", "Άλλο μάθημα", "Πίσω" })
                    });
            }

            return new DialogTurnResult(DialogTurnStatus.Cancelled);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is FoundChoice)
            {
                return (stepContext.Result as FoundChoice).Index switch
                {
                    0 => await stepContext.EndDialogAsync(null, cancellationToken),
                    1 => await stepContext.ReplaceDialogAsync(nameof(ExamsDialog) + "_" + nameof(WaterfallDialog)),
                    2 => await stepContext.ReplaceDialogAsync(nameof(ExamsDialog) + "_" + nameof(WaterfallDialog),
                            new Dictionary<string, int> { { "Course", (int)stepContext.Values["Course"] } }),
                    _ => new DialogTurnResult(DialogTurnStatus.Cancelled)
                };
            }

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
    }
}
