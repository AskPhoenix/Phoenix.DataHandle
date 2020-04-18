using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using static Phoenix.Bot.Helpers.DialogHelper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using System;
using Phoenix.DataHandle.Identity;
using Phoenix.Bot.Helpers;

namespace Phoenix.Bot.Dialogs
{
    public class AuthDialog : ComponentDialog
    {
        private readonly ApplicationDbContext _appContext;
        private readonly BotState _conversationState;
        private readonly BotState _userState;

        private static class WaterfallNames
        {
            public const string Main        = "Main_WaterfallDialog";
            public const string Phone       = "Phone_WaterfallDialog";
            public const string SendPin     = "SendPin_WaterfallDialog";
            public const string CheckPin    = "CheckPin_WaterfallDialog";
        }

        private static class PromptNames
        {
            public const string Phone = "PhoneNumber_Prompt";
            public const string Pin = "Pin_Prompt";
        }

        public AuthDialog(ApplicationDbContext appContext, ConversationState conversationState, UserState userState)
            : base(nameof(AuthDialog))
        {
            _appContext = appContext;
            _conversationState = conversationState;
            _userState = userState;

            AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));
            AddDialog(new NumberPrompt<long>(PromptNames.Phone, PhoneNumberPromptValidator));
            AddDialog(new NumberPrompt<long>(PromptNames.Pin, PinPromptValidator));

            AddDialog(new WaterfallDialog(WaterfallNames.Main,
                new WaterfallStep[]
                {
                    IntroStepAsync,
                    SignInStepAsync,
                    FinalStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Phone,
                new WaterfallStep[] 
                {
                    AskPhoneStepAsync,
                    CheckPhoneStepAsync,

                    PhoneNotFoundStepAsync,
                    PhoneRedirectStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.SendPin,
                new WaterfallStep[]
                {
                    SmsLeftCheckStepAsync,
                    SendPinStepAsync,
                    PinReceivedReplyStepAsync,

                    PinSendProblemStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.CheckPin,
                new WaterfallStep[]
                {
                    AskPinStepAsync,
                    CheckPinStepAsync
                }));

            InitialDialogId = WaterfallNames.Main;
        }

        #region Main Waterfall Dialog

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var card = new HeroCard
            {
                Title = "Καλωσόρισες στο Phoenix! 😁",
                Text = "Πάτησε ή πληκτρολόγησε \"Σύνδεση\" για να ξεκινήσουμε!",
                Tap = new CardAction(ActionTypes.OpenUrl, value: "https://www.askphoenix.gr"),
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.ImBack, title: "Σύνδεση", value: "Σύνδεση"),
                    new CardAction(ActionTypes.OpenUrl, title: "Μάθε περισσότερα...", value: "https://www.askphoenix.gr")
                }
            };

            var reply = (Activity)MessageFactory.Attachment(card.ToAttachment());
            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = reply,
                    RetryPrompt = reply,
                    Choices = new Choice[] { new Choice("Σύνδεση") },
                    Style = ListStyle.None
                });
        }

        private async Task<DialogTurnResult> SignInStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Αρχικά, θα χρειαστώ το κινητό τηλέφωνο επικοινωνίας που έχεις δώσει για την επαλήθευση.");
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.BeginDialogAsync(WaterfallNames.Phone, null, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.EndDialogAsync((bool?)stepContext.Result ?? false);

        #endregion

        #region Phone Waterfall Dialog

        private async Task<DialogTurnResult> AskPhoneStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.PromptAsync(
                PromptNames.Phone,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Παρακαλώ πληκτρολόγησε τον αριθμό παρακάτω:"),
                    RetryPrompt = MessageFactory.Text("Ο αριθμός τηλεφώνου πρέπει να είναι στη μορφή 69xxxxxxxx. Παρακαλώ πληκτρολόγησέ τον ξανά:")
                });

        private async Task<DialogTurnResult> CheckPhoneStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            long phone = (long)stepContext.Result;
            stepContext.Values.Add("phone", phone);

            //Identity
            //bool phoneFound = _appContext.Users.Any(user => user.PhoneNumber == phone.ToString());
            bool phoneFound = phone == 6912345678;
            if (phoneFound) 
            {
                await _conversationState.CreateProperty<string>("CheckedPhone").SetAsync(stepContext.Context, phone.ToString());
                return await stepContext.BeginDialogAsync(WaterfallNames.SendPin, phone, cancellationToken);
            }

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> PhoneNotFoundStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result != null)
                return await stepContext.EndDialogAsync(stepContext.Result, cancellationToken);

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Το κινητό τηλέφωνο που έγραψες δε βρέθηκε. " +
                        $"Είσαι σίγουρος ότι το {stepContext.Values["phone"]} είναι το σωστό;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ απάντησε με ένα Ναι ή Όχι:"),
                    Choices = new Choice[] { new Choice("Ναι"), new Choice("Όχι") }
                });
        }

        private async Task<DialogTurnResult> PhoneRedirectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("");
            var foundChoice = stepContext.Result as FoundChoice;
            if (foundChoice.Index == 0)
            {
                reply.Text = "Για να χρησιμοποιήσεις το Phoenix, θα πρέπει το φροντιστήριό σου να έχει πρώτα κάνει εγγραφή.";
                await stepContext.Context.SendActivityAsync(reply, cancellationToken);

                reply.Text = "Εάν πιστεύεις ότι κάτι είναι λάθος, μπορείς να επικοινωνήσεις με τους καθηγητές σου.";
                await stepContext.Context.SendActivityAsync(reply, cancellationToken);

                reply.Text = "Φυσικά, μπορείς να μάθεις περισσότερα για το Phoenix πατώντας τον παρακάτω σύνδεσμο: https://www.askphoenix.gr/";
                await stepContext.Context.SendActivityAsync(reply, cancellationToken);

                reply.Text = "Ελπίζω να τα ξαναπούμε σύντομα! Εις το επανιδείν! 😊";
                await stepContext.Context.SendActivityAsync(reply, cancellationToken);

                return await stepContext.EndDialogAsync(false, cancellationToken);
            }

            reply.Text = "Μην ανησυχείς, κανένα πρόβλημα!";
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            reply.Text = "Ας προσπαθήσουμε ξανά, πιο προσεκτικά!";
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            return await stepContext.ReplaceDialogAsync(stepContext.ActiveDialog.Id, stepContext.Options, cancellationToken);
        }

        #endregion

        #region Send Pin Waterfall Dialog

        private async Task<DialogTurnResult> SmsLeftCheckStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var smsAcsr = _userState.CreateProperty<int>("sms_left");
            int sms_left = await smsAcsr.GetAsync(stepContext.Context, () => 5);

            if (sms_left > 0)
            {
                await smsAcsr.SetAsync(stepContext.Context, sms_left - 1);
                return await stepContext.NextAsync(null, cancellationToken);
            }

            var reply = MessageFactory.Text("Δυστυχώς έχεις υπερβεί το όριο μηνυμάτων επαλήθευσης.");
            await stepContext.Context.SendActivityAsync(reply);

            reply.Text = "Παρακαλώ επικοινώνησε με τους καθηγητές σου για να συνεχίσεις.";
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> SendPinStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //TODO: Send pin to user with SMS
            long pin = new Random().Next(1000, 9999);
            stepContext.Values.Add("pin", pin);
            long phone = (long)stepContext.Options;

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Τέλεια! Μόλις σου έστειλα ένα SMS με ένα μοναδικό pin. Το έλαβες;"),
                    RetryPrompt = MessageFactory.Text("Έλαβες το SMS με το pin; Παρακαλώ απάντησε με ένα Ναι ή Όχι:"),
                    Choices = new Choice[] { new Choice("Ναι"), new Choice("Όχι") }
                });
        }

        private async Task<DialogTurnResult> PinReceivedReplyStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("");

            var foundChoice = stepContext.Result as FoundChoice;
            if (foundChoice.Index == 0)
            {
                reply.Text = "Ωραία! Για να ολοκληρωθεί η σύνδεση, θα χρειαστεί να γράψεις το pin που μόλις έλαβες.";
                await stepContext.Context.SendActivityAsync(reply);

                return await stepContext.BeginDialogAsync(WaterfallNames.CheckPin, stepContext.Values["pin"], cancellationToken);
            }

            reply.Text = "ΟΚ, μην ανησυχείς! Επειδή καμιά φορά αργεί να έρθει το μήνυμα, περίμενε μερικά λεπτά ακόμα.";
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Αν δεν έχει έρθει ακόμη, μπορώ να προσπαθήσω να σου ξαναστείλω. " +
                    "Αλλιώς, πάτησε \"Το έλαβα\" για να συνεχίσουμε:"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ή πληκτρολόγησε μία από τις παρακάτω απαντήσεις για να συνεχίσουμε:"),
                    Choices = new Choice[] { new Choice("Το έλαβα"), new Choice("Στείλε ξανά") }
                });
        }

        private async Task<DialogTurnResult> PinSendProblemStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Result is bool)
                return await stepContext.EndDialogAsync(stepContext.Result, cancellationToken);

            var foundChoice = stepContext.Result as FoundChoice;
            if (foundChoice.Index == 0)
                return await stepContext.BeginDialogAsync(WaterfallNames.CheckPin, stepContext.Values["pin"], cancellationToken);

            return await stepContext.ReplaceDialogAsync(stepContext.ActiveDialog.Id, stepContext.Options, cancellationToken);
        }

        #endregion

        #region Check Pin Waterfall Dialog

        private async Task<DialogTurnResult> AskPinStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
            => await stepContext.PromptAsync(
                PromptNames.Pin,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Παρακαλώ πληκτρολόγησε το pin που έλαβες παρακάτω:"),
                    RetryPrompt = MessageFactory.Text("Η μορφή του pin που πληκτρολόγησες δεν είναι έγκυρη. Παρακαλώ πληκτρολόγησέ το ξανά:"),
                    Validations = (long)Math.Ceiling(Math.Log10((long)stepContext.Options))
                });

        private async Task<DialogTurnResult> CheckPinStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            long pinTyped = (long)stepContext.Result;
            var reply = MessageFactory.Text("");

            //TODO: Check with real pin
            //bool pinOk = pinTyped == (long)stepContext.Options;
            bool pinOk = pinTyped == 1111;
            if (pinOk)
            {
                reply.Text = "Πολύ ωραία! Η σύνδεση ολοκληρώθηκε επιτυχώς! 😁";
                await stepContext.Context.SendActivityAsync(reply);

                return await stepContext.EndDialogAsync(true, cancellationToken);
            }

            reply.Text = "Το pin που έγραψες δεν είναι ίδιο με αυτό που σου έστειλα. Δες ξανά και προσεκτικά το SMS και προσπάθησε πάλι.";
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.ReplaceDialogAsync(stepContext.ActiveDialog.Id, stepContext.Options, cancellationToken);
        }

        #endregion
    }
}
