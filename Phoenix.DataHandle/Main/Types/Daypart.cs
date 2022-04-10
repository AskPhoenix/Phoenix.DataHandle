using Phoenix.Language.Types;

namespace Phoenix.DataHandle.Main.Types
{
    public enum Daypart
    {
        Never = 0,
        Now,
        Morning,        // 09:00
        Midday,         // 12:00
        Afternoon,      // 17:00
        Evening         // 20:00
    }

    public static class DaypartExtensions
    {
        public static string ToFriendlyString(this Daypart me)
        {
            return me switch
            {
                //Daypart.Never       => DaypartResources.Never,
                Daypart.Now         => DaypartResources.Now,
                Daypart.Morning     => DaypartResources.Morning,
                Daypart.Midday      => DaypartResources.Midday,
                Daypart.Afternoon   => DaypartResources.Afternoon,
                Daypart.Evening     => DaypartResources.Evening,
                _                   => string.Empty
            };
        }
    }
}
