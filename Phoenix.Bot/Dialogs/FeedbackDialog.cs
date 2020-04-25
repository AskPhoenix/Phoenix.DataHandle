using Microsoft.Bot.Builder.Dialogs;
using Phoenix.Bot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs
{
    public class FeedbackDialog : ComponentDialog
    {
        private static class WaterfallNames
        {
            public const string General     = "FeedbackGeneral_WaterfallDialog";
            public const string Specific    = "FeedbackSpecific_WaterfallDialog";
        }

        public FeedbackDialog()
            : base(nameof(FeedbackDialog))
        {
            AddDialog(new UnaccentedChoicePrompt(nameof(UnaccentedChoicePrompt)));

            AddDialog(new WaterfallDialog(WaterfallNames.General,
                new WaterfallStep[] 
                {

                }));

            AddDialog(new WaterfallDialog(WaterfallNames.Specific,
                new WaterfallStep[]
                {

                }));
        }

        protected override async Task<DialogTurnResult> OnBeginDialogAsync(DialogContext innerDc, object options, CancellationToken cancellationToken = default)
        {
            return await base.OnBeginDialogAsync(innerDc, options, cancellationToken);
        }
    }
}
