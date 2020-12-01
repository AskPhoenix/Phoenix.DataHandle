using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DataHandle.Main
{
    public enum BotFeedbackCategory
    {
        Empty = -1,
        Comment,
        Copliment,
        Suggestion,
        Rating,
        Complaint
    }

    public static class BotFeedbackCategoryExtensions
    {
        //TODO: Use locale

        private static readonly string[] CategoryEmojis = new string[]
        {
            "💬",
            "😊",
            "💡",
            "👍",
            "😒"
        };

        private static readonly string[] CategoryGreek = new string[]
        {
            "Κενή",
            "Γενικό σχόλιο",
            "Κοπλιμέντο",
            "Πρόταση ιδέας",
            "Αξιολόγηση",
            "Παράπονο"
        };

        public static string ToFriendlyString(this BotFeedbackCategory cat)
        {
            return cat switch
            {
                BotFeedbackCategory.Empty => "Empty",
                BotFeedbackCategory.Comment => "Comment",
                BotFeedbackCategory.Copliment => "Copliment",
                BotFeedbackCategory.Suggestion => "Suggestion",
                BotFeedbackCategory.Rating => "Rating",
                BotFeedbackCategory.Complaint => "Complaint",
                _ => string.Empty,
            };
        }

        public static IEnumerable<BotFeedbackCategory> GetAll()
        {
            return Enum.
                GetValues(typeof(BotFeedbackCategory)).
                Cast<BotFeedbackCategory>();
        }

        public static IEnumerable<string> GetAllNames(bool includeEmoji)
        {
            return Enum.
                GetValues(typeof(BotFeedbackCategory)).
                Cast<BotFeedbackCategory>().
                Select(c => includeEmoji && (int)c >= 0 ? CategoryEmojis[(int)c] : "" + " " + c.ToFriendlyString());
        }

        public static IEnumerable<string> GetAllGreekNames(bool includeEmoji)
        {
            return Enum.
                GetValues(typeof(BotFeedbackCategory)).
                Cast<BotFeedbackCategory>().
                Select(c => includeEmoji && (int)c >= 0 ? CategoryEmojis[(int)c] : "" + " " + CategoryGreek[(int)c]);
        }
    }
}
