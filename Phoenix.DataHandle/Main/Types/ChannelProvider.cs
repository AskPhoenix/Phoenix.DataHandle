namespace Phoenix.DataHandle.Main.Types
{
    public enum ChannelProvider
    {
        Unknown = 0,
        Facebook,
        Emulator
    }

    public static class ChannelProviderExtensions
    {
        public static ChannelProvider ToChannelProvider(this string me)
        {
            return Enum.GetValues<ChannelProvider>()
                .SingleOrDefault(cp => cp.ToString().Equals(me, StringComparison.OrdinalIgnoreCase));
        }

        public static bool TryToChannelProvider(this string me, out ChannelProvider channelProvider)
        {
            channelProvider = me.ToChannelProvider();

            return Enum.GetValues<ChannelProvider>()
                .Any(cp => cp.ToString().Equals(me, StringComparison.OrdinalIgnoreCase));
        }

        public static string ToFriendlyString(this ChannelProvider me)
        {
            return me switch
            {
                ChannelProvider.Facebook    => "Facebook",
                ChannelProvider.Emulator    => "Emulator",
                _                           => string.Empty
            };
        }
    }
}
