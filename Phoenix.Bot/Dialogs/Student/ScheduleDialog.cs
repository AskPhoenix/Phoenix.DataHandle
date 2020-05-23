using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Phoenix.Bot.Extensions;
using Phoenix.Bot.Helpers;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Phoenix.Bot.Helpers.CardHelper;

namespace Phoenix.Bot.Dialogs.Student
{
    public class ScheduleDialog : ComponentDialog
    {
        private readonly PhoenixContext _phoenixContext;

        private static class WaterfallNames
        {
            public const string Day = "StudentSchedule_Day_WaterfallDialog";
            public const string Date = "StudentSchedule_Date_WaterfallDialog";
            public const string Week = "StudentSchedule_Week_WaterfallDialog";
        }

        public ScheduleDialog(PhoenixContext phoenixContext)
            : base(nameof(ScheduleDialog))
        {
            _phoenixContext = phoenixContext;

            AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));

            AddDialog(new WaterfallDialog(WaterfallNames.Day,
                new WaterfallStep[]
                {
                    DayStepAsync,
                    DayOtherRedirectStepAsync,
                    DayOtherStepAsync,
                    DayResolveStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Date,
                new WaterfallStep[]
                {
                    //SpecificDateStepAsync,
                    //SpecificDateSelectStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Week,
                new WaterfallStep[]
                {
                    //WeekStepAsync
                    //WeekDayMoreStepAsync
                }));

            InitialDialogId = WaterfallNames.Day;
        }

        #region Day Waterfall Dialog

        private async Task<DialogTurnResult> DayStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            DateTime date = stepContext.Options is DateTime dt ? dt : DialogHelper.GreeceLocalTime();

            var lecs = _phoenixContext.Lecture.
                Include(l => l.Course).
                Include(l => l.Classroom).
                Where(l => l.StartDateTime.Date == date.Date).
                OrderBy(l => l.StartDateTime);

            await stepContext.Context.SendActivityAsync(new Activity(type: ActivityTypes.Typing));

            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 2));
            card.BackgroundImage = new AdaptiveBackgroundImage("https://www.bot.askphoenix.gr/assets/4f5d75_sq.png");
            card.Body.Add(new AdaptiveTextBlockHeaderLight($"Πρόγραμμα - {date:dddd} {date.Day}/{date.Month}"));

            foreach (var lec in lecs)
            {
                card.Body.Add(new AdaptiveTextBlockHeaderLight(lec.Course.Name));
                card.Body.Add(new AdaptiveRichFactSetLight("Ώρες ", $"{lec.StartDateTime:t} - {lec.EndDateTime:t}"));
                card.Body.Add(new AdaptiveRichFactSetLight("Αίθουσα ", lec.Classroom.Name, separator: true));
                card.Body.Add(new AdaptiveRichFactSetLight("Κατάσταση ", lec.Status.ToGreekString(), separator: true));
                card.Body.Add(new AdaptiveRichFactSetLight("Σχόλια ", string.IsNullOrEmpty(lec.Info) ? "-" : lec.Info, separator: true));
            }

            await stepContext.Context.SendActivityAsync(
                    MessageFactory.Attachment(new Attachment(contentType: AdaptiveCard.ContentType, content: JObject.FromObject(card))));

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Θα ήθελες να δεις το πρόγραμμα για άλλη ημέρα ή για ολόκληρη την εβδομάδα;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ απάντησε με ένα Ναι ή Όχι:"),
                    Choices = new Choice[] { new Choice("Άλλη ημέρα"), new Choice("Εβδομαδιαίο"), new Choice("Όχι, ευχαριστώ") { Synonyms = new List<string> { "Όχι" } } }
                });
        }

        private async Task<DialogTurnResult> DayOtherRedirectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var foundChoice = stepContext.Result as FoundChoice;
            if (foundChoice.Index == 2)
            {
                await stepContext.Context.SendActivityAsync("OK 😊");
                return await stepContext.EndDialogAsync(null, cancellationToken);
            }
            if (foundChoice.Index == 1)
                return await stepContext.BeginDialogAsync(WaterfallNames.Week, null, cancellationToken);

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> DayOtherStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var grNow = DialogHelper.GreeceLocalTime();
            var choices = new List<string>(8);
            for (int i = 1; i <= 7; i++)
            {
                DateTime nextDay = grNow.AddDays(i);
                choices.Add($"{nextDay:dddd} - {nextDay.Day}/{nextDay.Month}");
            }
            choices.Add("Άλλη ημερομηνία");

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Για ποια μέρα θα ήθελες να δεις το πρόγραμμά σου;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ χρησιμοποίησε μία από τις παρακάτω επιλογές:"),
                    Choices = ChoiceFactory.ToChoices(choices)
                });
        }

        private async Task<DialogTurnResult> DayResolveStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            int foundChoiceIndex = (stepContext.Result as FoundChoice).Index;
            if (foundChoiceIndex == 8)
                return await stepContext.BeginDialogAsync(WaterfallNames.Date, null, cancellationToken);

            DateTime date = DialogHelper.GreeceLocalTime().AddDays(foundChoiceIndex + 1);
            return await stepContext.ReplaceDialogAsync(WaterfallNames.Day, date, cancellationToken);
        }

        #endregion
    }
}
