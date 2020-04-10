using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1998

namespace Phoenix.Bot.Dialogs
{
    public class DialogExtensions
    {
        public static async Task<bool> UseExtraValidations(PromptValidatorContext<FoundChoice> promptContext, CancellationToken cancellationToken)
        {
            return promptContext.Recognized.Succeeded ||
                (promptContext.Options.Validations is IList<string> &&
                (promptContext.Options.Validations as IList<string>).Contains(promptContext.Context.Activity.Text));
        }
    }
}
