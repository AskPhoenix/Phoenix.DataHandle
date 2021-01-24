using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DataHandle.Main
{
    public enum BotFeedbackType
    {
        Empty = 0,
        Comment,
        Copliment,
        Suggestion,
        Rating,
        Complaint
    }

    public static class BotFeedbackTypeExtensions
    {
        //TODO: Use locale

        private static readonly string[] TypeEmojis = new string[]
        {
            "💬",
            "😊",
            "💡",
            "👍",
            "😒"
        };

        private static readonly string[] TypesGreek = new string[]
        {
            "Κενή",
            "Γενικό σχόλιο",
            "Κοπλιμέντο",
            "Πρόταση ιδέας",
            "Αξιολόγηση",
            "Παράπονο"
        };

        public static string ToFriendlyString(this BotFeedbackType cat)
        {
            return cat switch
            {
                BotFeedbackType.Empty       => "Empty",
                BotFeedbackType.Comment     => "Comment",
                BotFeedbackType.Copliment   => "Copliment",
                BotFeedbackType.Suggestion  => "Suggestion",
                BotFeedbackType.Rating      => "Rating",
                BotFeedbackType.Complaint   => "Complaint",
                _                               => string.Empty,
            };
        }

        public static IEnumerable<BotFeedbackType> GetAll(bool positiveOnly)
        {
            var values = Enum.
                GetValues(typeof(BotFeedbackType)).
                Cast<BotFeedbackType>();

            if (positiveOnly)
                values = values.Where(c => c > 0);

            return values;
        }

        public static IEnumerable<string> GetAllNames(bool positiveOnly, bool includeEmoji)
        {
            return GetAll(positiveOnly).
                Select(c => (includeEmoji && (int)c > 0 ? TypeEmojis[(int)c-1] : "") + " " + c.ToFriendlyString());
        }

        public static IEnumerable<string> GetAllGreekNames(bool positiveOnly, bool includeEmoji)
        {
            return GetAll(positiveOnly).
                Select(c => (includeEmoji && (int)c > 0 ? TypeEmojis[(int)c-1] : "") + " " + TypesGreek[(int)c]);
        }
    }
}
