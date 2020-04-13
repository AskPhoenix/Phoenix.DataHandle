using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using static Phoenix.Bot.Extensions.DialogExtensions;

namespace Phoenix.Bot.Dialogs.Student
{
    public class ScheduleDialog : ComponentDialog
    {
        public ScheduleDialog()
            : base(nameof(ScheduleDialog))
        {
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt), UseExtraValidations));
            AddDialog(new WaterfallDialog(nameof(ScheduleDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    DayScheduleStepAsync,
                    DaySelectStepAsync,
                    FinalStepAsync,
                    WeekScheduleStepAsync
                }));

            InitialDialogId = nameof(ScheduleDialog) + "_" + nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> DayScheduleStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            DayOfWeek selDay;
            if (stepContext.Options is Dictionary<string, object> && (stepContext.Options as Dictionary<string, object>).TryGetValue("Day", out object day))
                selDay = (DayOfWeek)day;
            else
            {
                //TODO: Take into consideration the holidays
                selDay = DateTime.Today.DayOfWeek;
                if (selDay == DayOfWeek.Saturday || selDay == DayOfWeek.Sunday)
                    selDay = DayOfWeek.Monday;
            }
            DateTimeFormatInfo grFormat = CultureInfo.GetCultureInfo("el-GR").DateTimeFormat;
            string dayName = grFormat.GetDayName(selDay);

            bool scheduleIsEmpty = false;

            //string dayArticle = (selDay == DayOfWeek.Monday) ? "τη" : "την";
            string scheduleText;
            if (!scheduleIsEmpty)
                scheduleText = $"- Αγγλικά   16:00 - 18:00\n\n- Γαλλικά   20:00 - 22:00";
            else
                scheduleText = "Σήμερα το πρόγραμμά σου είναι κενό! Απόλαυσε τη μέρα σου! 😊";

            var card = new HeroCard
            {
                Title = $"{dayName}",
                Text = scheduleText,
                Tap = new CardAction(ActionTypes.OpenUrl,
                    value: $"https://nuage.azurewebsites.net/extensions/student/schedule?day={(int)selDay}"),
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.ImBack, title: "Εβδομαδιαίο", value: "Εβδομαδιαίο"),
                    new CardAction(ActionTypes.ImBack, title: "Άλλη μέρα", value: "Άλλη μέρα")
                }
            };

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = (Activity)MessageFactory.Attachment(card.ToAttachment()),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ χρησιμοποίησε τα κουμπιά στην προηγούμενη κάρτα ή μία από τις παρακάτω επιλογές:"),
                    Choices = ChoiceFactory.ToChoices(new string[] { "Αρχικό μενού" }),
                    Validations = new string[] { "Άλλη μέρα", "Εβδομαδιαίο" }
                });
        }

        private async Task<DialogTurnResult> DaySelectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is FoundChoice)
            {
                return (stepContext.Result as FoundChoice).Index switch
                {
                    0 => await stepContext.EndDialogAsync(null, cancellationToken),
                    _ => new DialogTurnResult(DialogTurnStatus.Cancelled)
                };
            }
            else if (stepContext.Context.Activity.Text == "Άλλη μέρα")
            {
                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text("Για ποια μέρα θα ήθελες να δεις το πρόγραμμά σου;"),
                        RetryPrompt = MessageFactory.Text("Παρακαλώ χρησιμοποίησε μία από τις παρακάτω επιλογές:"),
                        Choices = ChoiceFactory.ToChoices(new string[] { "Δευτέρα", "Τρίτη", "Τετάρτη", "Πέμπτη", "Παρασκευή" })
                    });
            }
            else if (stepContext.Context.Activity.Text == "Εβδομαδιαίο")
            {
                return await WeekScheduleStepAsync(stepContext, cancellationToken);
            }

            return new DialogTurnResult(DialogTurnStatus.Cancelled);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is FoundChoice)
            {
                if ((stepContext.Result as FoundChoice).Value == "Αρχικό μενού")
                    return await stepContext.EndDialogAsync(null, cancellationToken);
                else if ((stepContext.Result as FoundChoice).Value == "Πίσω")
                    return await stepContext.ReplaceDialogAsync(nameof(ScheduleDialog) + "_" + nameof(WaterfallDialog));
                else
                    return await stepContext.ReplaceDialogAsync(nameof(ScheduleDialog) + "_" + nameof(WaterfallDialog),
                        new Dictionary<string, object> { { "Day", (DayOfWeek)(stepContext.Result as FoundChoice).Index + 1 } });
            }
            else
                return new DialogTurnResult(DialogTurnStatus.Cancelled);
        }

        private async Task<DialogTurnResult> WeekScheduleStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            DateTimeFormatInfo grFormat = CultureInfo.GetCultureInfo("el-GR").DateTimeFormat;

            var cards = new List<Attachment>(5);
            for (int i = 1; i <= 5; i++)
            {
                bool scheduleIsEmpty = new Random().Next(2) == 0;

                cards.Add(new HeroCard
                {
                    Title = grFormat.GetDayName((DayOfWeek)i).ToString(),
                    Text = (!scheduleIsEmpty) ? $"- Αγγλικά   16:00 - 18:00\n\n- Γαλλικά   20:00 - 22:00" :
                        "Το πρόγραμμά σου είναι κενό! Απόλαυσε τη μέρα σου! 😊",
                    Tap = new CardAction(ActionTypes.OpenUrl, 
                        value: "https://nuage.azurewebsites.net/extensions/student/schedule?req=week"),
                }.ToAttachment());
            }

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = (Activity)MessageFactory.Carousel(cards),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ χρησιμοποίησε μία από τις παρακάτω επιλογές:"),
                    Choices = ChoiceFactory.ToChoices(new string[] { "Αρχικό μενού", "Πίσω" })
                });
        }
    }
}
