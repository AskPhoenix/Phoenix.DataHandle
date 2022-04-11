using Phoenix.Language.Types;
using System;
using System.Linq;

namespace Phoenix.DataHandle.Main.Types
{
    public enum BotFeedbackCategory
    {
        None = 0,
        Comment,
        Compliment,
        Suggestion,
        Rating,
        Complaint
    }

    public static class BotFeedbackCategoryExtensions
    {
        public static string GetEmoji(this BotFeedbackCategory me)
        {
            return me switch
            {
                BotFeedbackCategory.Comment     => "💬",
                BotFeedbackCategory.Compliment  => "😊",
                BotFeedbackCategory.Suggestion  => "💡",
                BotFeedbackCategory.Rating      => "👍",
                BotFeedbackCategory.Complaint   => "😒",
                _                               => string.Empty
            };
        }

        public static string ToFriendlyString(this BotFeedbackCategory me)
        {
            return me switch
            {
                BotFeedbackCategory.Comment     => BotFeedbackCategoryResources.Comment,
                BotFeedbackCategory.Compliment  => BotFeedbackCategoryResources.Compliment,
                BotFeedbackCategory.Suggestion  => BotFeedbackCategoryResources.Suggestion,
                BotFeedbackCategory.Rating      => BotFeedbackCategoryResources.Rating,
                BotFeedbackCategory.Complaint   => BotFeedbackCategoryResources.Complaint,
                _                               => string.Empty
            };
        }

        public static string ToEmojiString(this BotFeedbackCategory me)
        {
            return me.GetEmoji() + " " + me.ToFriendlyString();
        }

        public static string[] GetFriendlyStrings()
        {
            return Enum.GetValues<BotFeedbackCategory>()
                .Select(v => v.ToFriendlyString())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }

        public static string[] GetEmojiStrings()
        {
            return Enum.GetValues<BotFeedbackCategory>()
                .Select(v => v.ToEmojiString())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }
    }
}
