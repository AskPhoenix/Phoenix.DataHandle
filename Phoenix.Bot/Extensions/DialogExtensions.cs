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

        public static Task<bool> PinPromptValidator(PromptValidatorContext<long> promptContext, CancellationToken cancellationToken)
        {
            long digits = (long)promptContext.Options.Validations;
            return Task.FromResult(promptContext.Recognized.Succeeded && (long)Math.Ceiling(Math.Log10(promptContext.Recognized.Value)) == digits);
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

        public static class Persistent
        {
            public static bool TryGetCommand(string text, out Command command)
            {
                command = text switch
                {
                    "--persistent-get-started--"    => Command.GetStarted,
                    "--persistent-home--"           => Command.Home,
                    "--persistent-tutorial--"       => Command.Tutorial,
                    _                               => Command.NoCommand
                };

                return command >= 0;
            }

            public static bool IsCommand(string text) => text.StartsWith("--persistent-") && text.EndsWith("--");

            public enum Command
            {
                NoCommand = -1,
                GetStarted,
                Home,
                Tutorial
            }
        }
    }
}
