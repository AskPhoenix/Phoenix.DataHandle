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

        public static DateTimeOffset GetDateTimeOffsetFromString(string dateTime, string format, IFormatProvider provider, TimeZoneInfo timeZone)
        {
            if (string.IsNullOrWhiteSpace(dateTime))
                throw new ArgumentNullException(nameof(dateTime));

            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentNullException(nameof(format));

            if (timeZone == null)
                throw new ArgumentNullException(nameof(timeZone));

            DateTime d = DateTime.ParseExact(dateTime, format, provider);
            return new DateTimeOffset(d, timeZone.GetUtcOffset(d));
        }

        public static DateTimeOffset GetDateTimeOffsetFromString(string dateTime, string format)
            => GetDateTimeOffsetFromString(dateTime, format, new CultureInfo("el-GR"), TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time"));
    }
}
