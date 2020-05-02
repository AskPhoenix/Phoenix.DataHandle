using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Bot.Helpers
{
    public static partial class DialogHelper
    {
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
            if (!name.EndsWith('ς'))
                return name;

            char letter = name.Substring(name.Length - 2, 1).ToCharArray()[0];

            if (letter == 'ο')
                letter = 'ε';
            else if (letter == 'ό')
                letter = 'έ';

            return name.Substring(0, name.Length - 2) + letter;
        }

        public static DateTime GreeceLocalTime()
               => TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time"));

        public static string ToUnaccented(this string str) => new string(str.Normalize(NormalizationForm.FormD).
            Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
    }
}
