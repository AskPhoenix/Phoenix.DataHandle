namespace Phoenix.DataHandle.Main
{
    public enum BroadcastVisibility
    {
        Hidden = 0,
        Group,
        Global
    }

    public static class BroadcastVisibilityExtensions
    {
        public static string ToFriendlyString(this BroadcastVisibility me)
        {
            return me switch
            {
                BroadcastVisibility.Group => "Τμήμα",
                BroadcastVisibility.Global => "Σε όλο το σχολείο",
                _ => string.Empty
            };
        }
    }
}
