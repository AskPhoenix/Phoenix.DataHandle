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
                .Single(cp => string.Equals(cp.ToString(), me, StringComparison.OrdinalIgnoreCase));
        }

        public static bool TryToChannelProvider(this string me, out ChannelProvider channelProvider)
        {
            try
            {
                channelProvider = me.ToChannelProvider();
                return true;
            }
            catch (InvalidOperationException)
            {
                channelProvider = ChannelProvider.Unknown;
            }

            return false;
        }
    }
}
