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

        public static TimeSpan CalculateTimeZoneOffset(string timeZone, DateTime dateTime)
        {
            return TimeZoneInfo.FindSystemTimeZoneById(timeZone).GetUtcOffset(dateTime);
        }

        public static DateTimeOffset SetOffsetFromTimeZone(this DateTimeOffset dateTimeOffset, string timeZone)
        {
            DateTime dt = dateTimeOffset.DateTime;
            return new DateTimeOffset(dt, CalculateTimeZoneOffset(timeZone, dt));
        }

        public static DateTimeOffset ParseExact(string input, string format, string timeZone)
        {
            var dateTime = CalendarExtensions.ParseExact(input, format);
            return new DateTimeOffset(dateTime, CalculateTimeZoneOffset(timeZone, dateTime));
        }

        public static DateTime ParseExact(string input, string format)
        {
            return DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
        }

        public static DateTimeOffset ParseTime(string input, string timeZone)
        {
            var dateTime = DateTime.ParseExact(input, "H:m", CultureInfo.InvariantCulture);

            var zeroDate = new DateTime();
            zeroDate = zeroDate.AddHours(dateTime.Hour);
            zeroDate = zeroDate.AddMinutes(dateTime.Minute);

            return new DateTimeOffset(zeroDate, CalculateTimeZoneOffset(timeZone, dateTime));
        }
    }
}
