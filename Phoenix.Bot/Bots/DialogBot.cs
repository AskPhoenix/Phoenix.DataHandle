using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Phoenix.Bot.Helpers;
using System.Threading;
using System.Threading.Tasks;
using static Phoenix.Bot.Helpers.DialogHelper;

namespace Phoenix.Bot.Bots
{
    public class DialogBot<T> : ActivityHandler where T : Dialog
    {
        protected readonly Dialog Dialog;
        protected readonly BotState ConversationState;
        protected readonly BotState UserState;

        public DialogBot(ConversationState conversationState, UserState userState, T dialog)
        {
            ConversationState = conversationState;
            UserState = userState;
            Dialog = dialog;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            if (turnContext.Activity.Text == null)
                turnContext.Activity.Text = string.Empty;
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occured during the turn.
            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string mess = turnContext.Activity.Text;
            
            bool resetConversation = Persistent.IsCommand(mess) 
                || mess.ContainsSynonyms(SynonymHelper.Topics.Greetings)
                || (mess.ContainsSynonyms(SynonymHelper.Topics.Help) 
                    && await UserState.CreateProperty<bool>("IsAuthenticated").GetAsync(turnContext));

            if (resetConversation)
                await ConversationState.ClearStateAsync(turnContext, cancellationToken);

            //await turnContext.SendActivityAsync(new Activity(type: ActivityTypes.Typing));
            await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        }
    }
}
