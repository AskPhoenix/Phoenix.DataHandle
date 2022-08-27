using Phoenix.Language.Types.Daypart;

namespace Phoenix.DataHandle.Main.Types
{
    public enum Daypart
    {
        Never = 0,
        Now,
        Morning,
        Midday,
        Afternoon,
        Evening
    }

    public static class DaypartStartingHours
    {
        public const int MorningStartingHour    = 9;
        public const int MiddayStartingHour     = 12;
        public const int AfternoonStartingHour  = 17;
        public const int EveningStartingHour    = 20;
    }

    public static class DaypartExtensions
    {
        public static bool IsSlotDaypart(this Daypart me) => me >= Daypart.Morning;

        public static Daypart[] AllDayparts => Enum.GetValues<Daypart>();
        public static Daypart[] SlotDayparts => AllDayparts.Where(dp => dp.IsSlotDaypart()).ToArray();

        public static string ToFriendlyString(this Daypart me)
        {
            return me switch
            {
                Daypart.Now         => DaypartResources.Now,
                Daypart.Morning     => DaypartResources.Morning,
                Daypart.Midday      => DaypartResources.Midday,
                Daypart.Afternoon   => DaypartResources.Afternoon,
                Daypart.Evening     => DaypartResources.Evening,
                _                   => string.Empty
            };
        }

        public static string[] GetFriendlyStrings()
        {
            return AllDayparts
                .Select(v => v.ToFriendlyString())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }

        public static Daypart[] FindFutureDayparts(DateTimeOffset reference)
        {
            List<Daypart> parts = new(AllDayparts);
            parts.Remove(Daypart.Never);

            int hour = reference.Hour;
            
            if (hour > DaypartStartingHours.MorningStartingHour)
                parts.Remove(Daypart.Morning);
            if (hour > DaypartStartingHours.MiddayStartingHour)
                parts.Remove(Daypart.Midday);
            if (hour > DaypartStartingHours.AfternoonStartingHour)
                parts.Remove(Daypart.Afternoon);
            if (hour > DaypartStartingHours.EveningStartingHour)
                parts.Remove(Daypart.Evening);

            return parts.ToArray();
        }
    }
}
