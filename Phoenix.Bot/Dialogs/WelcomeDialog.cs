using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Bot.Dialogs
{
    public class WelcomeDialog : ComponentDialog
    {
        public WelcomeDialog()
            : base(nameof(WelcomeDialog))
        {
            AddDialog(new WaterfallDialog(nameof(WelcomeDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    
                }));

            InitialDialogId = nameof(WelcomeDialog) + "_" + nameof(WaterfallDialog);
        }
    }
}
