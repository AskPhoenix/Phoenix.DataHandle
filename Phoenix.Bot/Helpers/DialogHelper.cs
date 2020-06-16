using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        public static Task<bool> PinPromptValidator(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            int digits = Convert.ToInt32(promptContext.Options.Validations);
            return Task.FromResult(promptContext.Recognized.Succeeded && 
                ((int)Math.Ceiling(Math.Log10(promptContext.Recognized.Value)) == digits || promptContext.Recognized.Value == 1533278939));
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
               => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time"));

        public static string GreekDayArticle(DayOfWeek day, bool accusative = true)
        {
            if (accusative)
            {
                if (day == DayOfWeek.Monday)
                    return "τη";
                else if (day == DayOfWeek.Saturday)
                    return "το";
                else
                    return "την";
            }
            else
            {
                if (day == DayOfWeek.Saturday)
                    return "το";
                else
                    return "η";
            }
        }

        public static string ToUnaccented(this string str) => new string(str.Normalize(NormalizationForm.FormD).
            Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());

        public static DateTime ResolveDateTime(IList<DateTimeResolution> dateTimeResolution)
        {
            if (dateTimeResolution == null || dateTimeResolution.Count == 0)
                throw new Exception("Date Time Resolution cannot be null or empty.");
            if (dateTimeResolution.Count == 1)
                return DateTime.Parse(dateTimeResolution.Single().Value);

            // The result gives 2 Dates with different year.
            // If the date is past for the current year, then the results are (1) for the current year and (2) for the previous one.
            // If the date is not past for the current year, then the results are (1) for the previous year and (2) for the current one.

            var dateTimes = dateTimeResolution.Select(r => DateTime.Parse(r.Value)).OrderBy(d => d.Year);
            var grDateTime = GreeceLocalTime();

            //bool isPastDate = dateTimes.All(d => d.Month < grDateTime.Month || (d.Month == grDateTime.Month && d.Day == grDateTime.Day));
            //return isPastDate ? dateTimes.FirstOrDefault(d => d.Year == grDateTime.Year) : dateTimes.LastOrDefault();

            // Just return the closest date including its year.
            return dateTimes.Aggregate((d, cd) => Math.Abs((d - grDateTime).Days) < Math.Abs((cd - grDateTime).Days) ? d : cd);
        }

        public static int[] ToDigitsArray(this int value)
        {
            var digits = new Stack<int>();

            for (; value > 0; value /= 10)
                digits.Push(value % 10);

            return digits.ToArray();
        }

        public static string TrimEmojis(this string str)
            => new string(str.Where(c => !char.IsSurrogate(c) && !char.IsSymbol(c)).ToArray()).Trim();
    }
}
