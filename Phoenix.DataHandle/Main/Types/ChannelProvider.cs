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
        private static bool ChannelProviderPredicate(ChannelProvider cp, string str) =>
            cp.ToString().Equals(str, StringComparison.OrdinalIgnoreCase);

        public static ChannelProvider ToChannelProvider(this string me)
        {
            return Enum.GetValues<ChannelProvider>().SingleOrDefault(cp => ChannelProviderPredicate(cp, me));
        }

        public static bool TryToChannelProvider(this string me, out ChannelProvider channelProvider)
        {
            channelProvider = me.ToChannelProvider();

            return Enum.GetValues<ChannelProvider>().Any(cp => ChannelProviderPredicate(cp, me));
        }
    }
}
