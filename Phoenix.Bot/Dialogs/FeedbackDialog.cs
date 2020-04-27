using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.EntityFrameworkCore;
using Phoenix.Bot.Extensions;
using Phoenix.Bot.Helpers;
using Phoenix.DataHandle.Main.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS8509

namespace Phoenix.Bot.Dialogs
{
    public class FeedbackDialog : ComponentDialog
    {
        private readonly PhoenixContext _phoenixContext;

        private BotFeedback BotFeedback { get; set; }

        private static class WaterfallNames
        {
            public const string Spontaneous = "FeedbackSpontaneous_WaterfallDialog";
            public const string Triggered   = "FeedbackTriggered_WaterfallDialog";
            public const string Comment     = "FeedbackComment_WaterfallDialog";
            public const string Rating      = "FeedbackRating_WaterfallDialog";
        }

        public FeedbackDialog(PhoenixContext phoenixContext)
            : base(nameof(FeedbackDialog))
        {
            _phoenixContext = phoenixContext;

            AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));

            AddDialog(new WaterfallDialog(WaterfallNames.Spontaneous,
                new WaterfallStep[] 
                {
                    CategoryStepAsync,
                    RedirectStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Triggered,
                new WaterfallStep[]
                {
                    AskForFeedbackStepAsync,
                    ReplyFeedbackStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Rating,
                new WaterfallStep[]
                {
                    RatingPromptStepAsync,
                    RatingReplyStepAsync
                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Comment,
                new WaterfallStep[]
                {
                    CommentPromptStepAsync,
                    CommentReplyStepAsync
                }));
        }

        protected override async Task<DialogTurnResult> OnBeginDialogAsync(DialogContext innerDc, object options, CancellationToken cancellationToken = default)
        {
            BotFeedback = new BotFeedback()
            {
                AuthorId = _phoenixContext.AspNetUsers.Single(u => u.FacebookId == innerDc.Context.Activity.From.Id).Id
            };

            if (Persistent.TryGetCommand(innerDc.Context.Activity.Text, out Persistent.Command cmd) && cmd == Persistent.Command.Feedback)
            {
                BotFeedback.Occasion = Feedback.Occasion.Persistent_Menu.ToString();
                InitialDialogId = WaterfallNames.Spontaneous;
            }
            else
            {
                BotFeedback.Occasion = ((Feedback.Occasion)options).ToString();
                InitialDialogId = WaterfallNames.Triggered;
            }

            return await base.OnBeginDialogAsync(innerDc, options, cancellationToken);
        }

        protected override Task OnEndDialogAsync(ITurnContext context, DialogInstance instance, DialogReason reason, CancellationToken cancellationToken = default)
        {
            try
            {
                _phoenixContext.Add(BotFeedback);
                _phoenixContext.SaveChanges();
            } 
            catch (DbUpdateException) { }

            return base.OnEndDialogAsync(context, instance, reason, cancellationToken);
        }

        #region Feedback Spontaneous Waterfall Dialog

        private async Task<DialogTurnResult> CategoryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync("Τα σχόλιά σου είναι πολύτιμα για να γίνει το Phoenix ακόμα καλύτερο! 😁");
            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Τι είδους σχόλιο θα ήθελες να κάνεις;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε μία από τις παρακάτω κατηγορίες:"),
                    Choices = ChoiceFactory.ToChoices(Feedback.CategoriesGreek)
                });
        }

        private async Task<DialogTurnResult> RedirectStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var selCat = (Feedback.Category)(stepContext.Result as FoundChoice).Index;
            BotFeedback.Category = selCat.ToString();

            if (selCat == Feedback.Category.Rating)
                return await stepContext.BeginDialogAsync(WaterfallNames.Rating, null, cancellationToken);
            
            return await stepContext.BeginDialogAsync(WaterfallNames.Comment, selCat, cancellationToken);
        }

        #endregion

        #region Feedback Triggered Waterfall Dialog

        private async Task<DialogTurnResult> AskForFeedbackStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Θα ήθελες να κάνεις ένα σχόλιο για τη μέχρι τώρα εμπειρία σου στο Phoenix; 😊"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ απάντησε με ένα Ναι ή Όχι:"),
                    Choices = new Choice[] { new Choice("Ναι"), new Choice("Όχι, ευχαριστώ") { Synonyms = new List<string> { "Όχι" } } }
                });
        }

        private async Task<DialogTurnResult> ReplyFeedbackStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var foundChoice = stepContext.Result as FoundChoice;
            if (foundChoice.Index == 0)
                return await stepContext.BeginDialogAsync(WaterfallNames.Comment, Feedback.Category.Comment, cancellationToken);

            await stepContext.Context.SendActivityAsync("Εντάξει! Ίσως μια άλλη φορά!");
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        #endregion

        #region Feedback Rating Waterfall Dialog

        private async Task<DialogTurnResult> RatingPromptStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(
                nameof(UnaccentedChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Πώς με βαθμολογείς;"),
                    RetryPrompt = MessageFactory.Text("Παρακαλώ επίλεξε ένα από τα παρακάτω εικονίδια:"),
                    Choices = ChoiceFactory.ToChoices(new string[] { "😍", "😄", "🙂", "😐", "😒" })
                });
        }

        private async Task<DialogTurnResult> RatingReplyStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            BotFeedback.Rating = (byte)(5 - (stepContext.Result as FoundChoice).Index);

            await stepContext.Context.SendActivityAsync("Σ' ευχαριστώ πολύ για τη βαθμολογία σου! 😊");
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        #endregion

        #region Feedback Comment Waterfall Dialog

        private async Task<DialogTurnResult> CommentPromptStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var reply = (Feedback.Category)stepContext.Options switch
            {
                Feedback.Category.Comment => MessageFactory.Text("Ωραία! Σε ακούω:"),
                Feedback.Category.Copliment => MessageFactory.Text("Τέλεια!! 😍 Ανυπομονώ να ακούσω:"),
                Feedback.Category.Suggestion => MessageFactory.Text("Ανυπομονώ να ακούσω την ιδέα σου:"),
                Feedback.Category.Complaint => MessageFactory.Text("Λυπάμαι αν σε στενοχώρησα 😢 Πες μου τι σε ενόχλησε:")
            };

            return await stepContext.PromptAsync(
                nameof(TextPrompt),
                new PromptOptions
                {
                    Prompt = reply,
                    RetryPrompt = MessageFactory.Text("Παρακαλώ γράψε το σχόλιό σου παρακάτω:")
                });
        }

        private async Task<DialogTurnResult> CommentReplyStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            BotFeedback.Comment = (string)stepContext.Result;

            await stepContext.Context.SendActivityAsync("Σ' ευχαριστώ πολύ για το σχόλιό σου! 😊");
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

        #endregion
    }
}
