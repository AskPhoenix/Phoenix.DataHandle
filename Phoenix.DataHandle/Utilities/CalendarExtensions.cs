using System;
using System.Globalization;

namespace Phoenix.DataHandle.Utilities
{
    public static class CalendarExtensions
    {
        public static int GetWeekOfYearISO8601(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static int GetWeekOfYearISO8601(DateTimeOffset date) => GetWeekOfYearISO8601(date.DateTime);

        public static DateTimeOffset ParseExact(string input, string format, string timeZone)
        {
            if (string.IsNullOrWhiteSpace(timeZone))
                throw new ArgumentNullException(nameof(timeZone));

            var dateTime = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
            return new DateTimeOffset(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZone).GetUtcOffset(dateTime));
        }
    }
}
