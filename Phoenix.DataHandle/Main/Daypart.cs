namespace Phoenix.DataHandle.Main
{
    public enum Daypart
    {
        Now = 0,
        Morning,        // 09:00
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
                Daypart.Now => "Τώρα",
                Daypart.Morning => "Πρωί",
                Daypart.Midday => "Μεσημέρι",
                Daypart.Afternoon => "Απόγευμα",
                Daypart.Evening => "Βραδάκι",
                _ => string.Empty
            };
        }
    }
}
