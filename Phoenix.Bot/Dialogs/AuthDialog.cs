using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Phoenix.DataHandle;
using Phoenix.DataHandle.Models;
using static Phoenix.Bot.Extensions.DialogExtensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;

namespace Phoenix.Bot.Dialogs
{
    public class AuthDialog : ComponentDialog
    {
        public AuthDialog()
            : base(nameof(AuthDialog))
        {
            AddDialog(new NumberPrompt<long>("PhoneNumberPrompt", PhoneNumberPromptValidator));
            AddDialog(new NumberPrompt<long>("PinPrompt", PinPromptValidator));
            AddDialog(new WaterfallDialog(nameof(AuthDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    PhoneStepAsync,
                    PinStepAsync,
                    FinalStepAsync
                }));

            InitialDialogId = nameof(AuthDialog) + "_" + nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> PhoneStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (stepContext.Options is long)
                return await stepContext.NextAsync(stepContext.Options, cancellationToken);
            else if (stepContext.Options is string && (string)stepContext.Options == "new_phone")
                return await NewPhoneStepAsync(stepContext, cancellationToken);

            //TODO: Use DB User name
            var reply = MessageFactory.Text($"Καλωσόρισες στο Phoenix {GreekNameCall(stepContext.Context.Activity.From.Name.Split(' ')[0])}! 😁");
            await stepContext.Context.SendActivityAsync(reply);

            reply = MessageFactory.Text("Προτού ξεκινήσουμε, θα πρέπει να σιγουρευτώ ότι είσαι εσύ!");
            await stepContext.Context.SendActivityAsync(reply);

            reply = MessageFactory.Text("Αρχικά, θα χρειαστώ το κινητό τηλέφωνο επικοινωνίας που έχεις δώσει για την επαλήθευση: (π.χ. 69xxxxxxxx)");
            return await stepContext.PromptAsync(
                "PhoneNumberPrompt",
                new PromptOptions
                {
                    Prompt = reply,
                    RetryPrompt = MessageFactory.Text("Παρακαλώ πληκτρολόγησε τον αριθμό τηλεφώνου σου στη μορφή 69xxxxxxxx:")
                });
        }

        private async Task<DialogTurnResult> NewPhoneStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("Παρακαλώ πληκτρολόγησε τον αριθμό τηλεφώνου σου: (π.χ. 69xxxxxxxx)");
            return await stepContext.PromptAsync(
                "PhoneNumberPrompt",
                new PromptOptions
                {
                    Prompt = reply,
                    RetryPrompt = MessageFactory.Text("Παρακαλώ πληκτρολόγησε τον αριθμό τηλεφώνου σου στη μορφή 69xxxxxxxx:")
                });
        }

        private async Task<DialogTurnResult> PinStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            long phone = (long)stepContext.Result;

            //TODO: Search for phone in DB
            bool phoneFound = true;
            if (phoneFound)
            {
                //TODO: Generate pin and send it to user's phone
                long pin = 1111;

                var reply = (Activity) MessageFactory.SuggestedActions( new string[] { "Αποστολή ξανά", "Αλλαγή αριθμού" },
                    text: "Τέλεια! Μόλις σου έστειλα ένα μοναδικό pin με SMS. Παρακαλώ πληκτρολόγησέ το, ώστε να ολοκληρωθεί η επαλήθευση:");
                return await stepContext.PromptAsync(
                    "PinPrompt",
                    new PromptOptions
                    {
                        Prompt = reply,
                        RetryPrompt = (Activity) MessageFactory.SuggestedActions(new string[] { "Αποστολή ξανά", "Αλλαγή αριθμού" }, 
                            text: "Παρακαλώ πληκτρολόγησε σωστά το pin που έχει αποσταλεί στον αριθμό τηλεφώνου σου:"),
                        Validations = pin,
                        Choices = new List<Choice>() { new Choice("Αποστολή ξανά"), new Choice("Αλλαγή αριθμού") }
                    });
            }
            else
            {
                var reply = MessageFactory.Text("Το κινητό τηλέφωνο δε βρέθηκε. Για να συνδεθείς στο Phoenix, " +
                    "θα πρέπει το φροντιστήριό σου να έχει πρώτα εγγραφεί. " +
                    "Παρακαλώ επικοινώνησε με τη γραμματεία ή τους καθηγητές σου.");
                await stepContext.Context.SendActivityAsync(reply);

                reply = MessageFactory.Text("Για περισσότερες πληροφορίες μπορείς να επισκεφθείς τον παρακάτω σύνδεσμο: https://www.askphoenix.gr");
                await stepContext.Context.SendActivityAsync(reply);

                reply = MessageFactory.Text("Εις το επανηδείν! 😊");
                await stepContext.Context.SendActivityAsync(reply);

                return await stepContext.EndDialogAsync(false);
            }
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string mess = stepContext.Context.Activity.Text.ToLower();

            if (mess == "αποστολή ξανά")
                //TODO: Replace with phone from DB
                return await stepContext.ReplaceDialogAsync(this.InitialDialogId, 6912345678, cancellationToken);
            else if (mess == "αλλαγή αριθμού")
                return await stepContext.ReplaceDialogAsync(this.InitialDialogId, "new_phone", cancellationToken);

            return await stepContext.EndDialogAsync(true);
        }
    }
}
