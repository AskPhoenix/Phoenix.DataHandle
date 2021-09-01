namespace Phoenix.DataHandle.Main
{
    public enum Daypart
    {
        Morning = 0,    // 09:00
        Midday,         // 12:00
        Afternoon,      // 17:00
        Evening         // 20:00
    }

    public static class DaypartExtensions
    {
        //TODO: Locale
        public static string ToFriendlyString(this Daypart me)
        {
            return me switch
            {
                Daypart.Morning => "Πρωί",
                Daypart.Midday => "Μεσημέρι",
                Daypart.Afternoon => "Απόγευμα",
                Daypart.Evening => "Βραδάκι",
                _ => string.Empty
            };
        }
    }
}
