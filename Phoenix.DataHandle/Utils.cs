using System;
using System.Globalization;

namespace Phoenix.DataHandle
{
    public class Utils
    {
        public static int GetWeekOfYearISO8601(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static int GetWeekOfYearISO8601(DateTimeOffset date)
        {
            return GetWeekOfYearISO8601(date.DateTime);
        }
    }
}
