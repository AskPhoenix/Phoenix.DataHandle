using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Extensions
{
    public static class DialogExtensions
    {
        public static Task<bool> UseExtraValidations(PromptValidatorContext<FoundChoice> promptContext, CancellationToken cancellationToken)
        {
            return Task.FromResult
                (promptContext.Recognized.Succeeded ||
                (promptContext.Options.Validations is IList<string> &&
                (promptContext.Options.Validations as IList<string>).Contains(promptContext.Context.Activity.Text)));
        }

        public static Task<bool> PhoneNumberPromptValidator(PromptValidatorContext<long> promptContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                promptContext.Recognized.Succeeded && 
                promptContext.Recognized.Value > 0 &&
                (Math.Ceiling(Math.Log10(promptContext.Recognized.Value)) == 10 && promptContext.Recognized.Value / 100000000 == 69) ||
                (Math.Ceiling(Math.Log10(promptContext.Recognized.Value)) == 12 && promptContext.Recognized.Value / 100000000 == 3069));
        }

        public static Task<bool> PinPromptValidator(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            int pin = (int)promptContext.Options.Validations;
            string mess = promptContext.Context.Activity.Text.ToLower();

            return Task.FromResult(
                (promptContext.Recognized.Succeeded && promptContext.Recognized.Value == pin) ||
                (!promptContext.Recognized.Succeeded && (mess == "αποστολή ξανά" || mess == "αλλαγή αριθμού")));
        }

        public static async Task<string> ReceiveGifAsync(string rating, string query, int limit, int? offset, string key)
        {
            string giphyUrl = "http://api.giphy.com/v1/gifs/search" + $"?rating={rating}&q={query}&limit={limit}&offset={offset}&api_key={key}";
            string response;

            using (var httpClient = new HttpClient())
            {
                response = await httpClient.GetAsync(giphyUrl).Result.Content.ReadAsStringAsync();
            }

            return JObject.Parse(response)["data"].First["images"]["downsized"]["url"].ToString();
        }

        public static string GreekNameCall(string name)
        {
            string tore;

            tore = name.Replace("ος", "ε");
            tore = tore.Replace("ός", "έ");
            tore = tore.Replace("ης", "η");
            tore = tore.Replace("ής", "ή");
            tore = tore.Replace("ας", "α");
            tore = tore.Replace("άς", "ά");

            return tore;
        }
    }
}
