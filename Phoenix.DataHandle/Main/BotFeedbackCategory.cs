using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DataHandle.Main
{
    public enum BotFeedbackCategory
    {
        Empty = 0,
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
                BotFeedbackCategory.Empty       => "Empty",
                BotFeedbackCategory.Comment     => "Comment",
                BotFeedbackCategory.Copliment   => "Copliment",
                BotFeedbackCategory.Suggestion  => "Suggestion",
                BotFeedbackCategory.Rating      => "Rating",
                BotFeedbackCategory.Complaint   => "Complaint",
                _                               => string.Empty,
            };
        }

        public static IEnumerable<BotFeedbackCategory> GetAll(bool positiveOnly)
        {
            var values = Enum.
                GetValues(typeof(BotFeedbackCategory)).
                Cast<BotFeedbackCategory>();

            if (positiveOnly)
                values = values.Where(c => c > 0);

            return values;
        }

        public static IEnumerable<string> GetAllNames(bool positiveOnly, bool includeEmoji)
        {
            return GetAll(positiveOnly).
                Select(c => (includeEmoji && (int)c > 0 ? CategoryEmojis[(int)c-1] : "") + " " + c.ToFriendlyString());
        }

        public static IEnumerable<string> GetAllGreekNames(bool positiveOnly, bool includeEmoji)
        {
            return GetAll(positiveOnly).
                Select(c => (includeEmoji && (int)c > 0 ? CategoryEmojis[(int)c-1] : "") + " " + CategoryGreek[(int)c]);
        }
    }
}
