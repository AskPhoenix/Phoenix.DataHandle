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

        private static readonly string[] CategoriesGreek = new string[]
        {
            "💬 Γενικό σχόλιο",
            "😊 Κοπλιμέντο",
            "💡 Πρόταση ιδέας",
            "👍 Αξιολόγηση",
            "😒 Παράπονο"
        };

        public static string ToFriendlyString(this BotFeedbackCategory cat)
        {
            switch (cat)
            {
                case BotFeedbackCategory.Empty:
                    return "Empty";
                case BotFeedbackCategory.Comment:
                    return "Comment";
                case BotFeedbackCategory.Copliment:
                    return "Copliment";
                case BotFeedbackCategory.Suggestion:
                    return "Suggestion";
                case BotFeedbackCategory.Rating:
                    return "Rating";
                case BotFeedbackCategory.Complaint:
                    return "Complaint";
                default:
                    return string.Empty;
            }
        }

        public static string[] GetCategoryNames(bool includeEmoji = true)
        {
            if (includeEmoji)
                return CategoriesGreek;

            return CategoriesGreek.Select(c => c.Substring(2)).ToArray();
        }
    }
}
