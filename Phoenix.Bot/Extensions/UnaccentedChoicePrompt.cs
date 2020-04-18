using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Extensions
{
    public class UnaccentedChoicePrompt : ChoicePrompt
    {
        public UnaccentedChoicePrompt(string dialogId, PromptValidator<FoundChoice> validator = null, string defaultLocale = null)
            : base(dialogId, validator, defaultLocale) 
        { 
        
        }

        public UnaccentedChoicePrompt(string dialogId, Dictionary<string, ChoiceFactoryOptions> choiceDefaults, PromptValidator<FoundChoice> validator = null, string defaultLocale = null)
            : base(dialogId, choiceDefaults, validator, defaultLocale)
        {
            
        }

        protected override Task<PromptRecognizerResult<FoundChoice>> OnRecognizeAsync(ITurnContext turnContext, IDictionary<string, object> state, PromptOptions options, CancellationToken cancellationToken = default)
        {
            //Adds the unaccented version of every choice as its synonym
            for (int i = 0; i < options.Choices.Count; i++)
            {
                string unaccented = new string(options.Choices[i].Value.Normalize(NormalizationForm.FormD).
                    Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());

                if (options.Choices[i].Synonyms == null)
                    options.Choices[i].Synonyms = new List<string> { unaccented };
                else if (!options.Choices[i].Synonyms.Contains(unaccented))
                    options.Choices[i].Synonyms.Add(unaccented);
            }

            return base.OnRecognizeAsync(turnContext, state, options, cancellationToken);
        }
    }
}
