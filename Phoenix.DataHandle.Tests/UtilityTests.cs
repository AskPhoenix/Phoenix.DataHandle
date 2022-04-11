using Phoenix.DataHandle.Main.Types;
using System.Globalization;
using System.Threading;
using Xunit;

namespace Phoenix.DataHandle.Tests
{
    public class UtilityTests
    {
        public UtilityTests()
        {
        }

        [Fact]
        public void EnumExtensionsTest()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            string[] strs;
            for (int i = 0; i < 2; i++)
            {
                strs = BotFeedbackCategoryExtensions.GetFriendlyStrings();
                strs = BroadcastAudienceExtensions.GetFriendlyStrings();
                strs = BroadcastVisibilityExtensions.GetFriendlyStrings();
                strs = DaypartExtensions.GetFriendlyStrings();
                strs = RoleExtensions.GetFriendlyStrings();

                strs = BotFeedbackCategoryExtensions.GetEmojiStrings();

                Thread.CurrentThread.CurrentUICulture = new CultureInfo("el-GR"); // el
            }
        }
    }
}
