using System;
using System.Linq;

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
                .SingleOrDefault(cp => string.Equals(cp.ToString(), me, StringComparison.OrdinalIgnoreCase));
        }

        public static bool TryToChannelProvider(this string me, out ChannelProvider channelProvider)
        {
            channelProvider = me.ToChannelProvider();

            return Enum.GetValues<ChannelProvider>()
                .Any(cp => string.Equals(cp.ToString(), me, StringComparison.OrdinalIgnoreCase));
        }
    }
}
