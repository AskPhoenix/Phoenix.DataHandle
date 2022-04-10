using Phoenix.Language.Types;

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
    }
}
