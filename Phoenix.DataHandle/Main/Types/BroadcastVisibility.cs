using Phoenix.Language.Types.BroadcastVisibility;

namespace Phoenix.DataHandle.Main.Types
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
                BroadcastVisibility.Group   => BroadcastVisibilityResources.Group,
                BroadcastVisibility.Global  => BroadcastVisibilityResources.Global,
                _                           => string.Empty
            };
        }

        public static string[] GetFriendlyStrings()
        {
            return Enum.GetValues<BroadcastVisibility>()
                .Select(v => v.ToFriendlyString())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }
    }
}
