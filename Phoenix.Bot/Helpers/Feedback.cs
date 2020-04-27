namespace Phoenix.Bot.Helpers
{
    public static class Feedback
    {
        public enum Category
        {
            Comment,
            Copliment,
            Suggestion,
            Complaint,
            Rating
        }

        public enum Occasion
        {
            Student_Exercise,
            Student_Exam,
            Student_Schedule,
            Persistent_Menu
        }

        public static readonly string[] CategoriesGreek = new string[] { "Γενικό σχόλιο", "Κοπλιμέντο", "Πρόταση ιδέας", "Αξιολόγηση", "Παράπονο" };

        public static string ToStringGreek(this Category cat) => CategoriesGreek[(int)cat];
    }
}
