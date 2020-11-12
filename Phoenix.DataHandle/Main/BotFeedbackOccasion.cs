namespace Phoenix.DataHandle.Main
{
    public enum BotFeedbackOccasion
    {
        Student_Exercise,
        Student_Exam,
        Student_Schedule,
        Persistent_Menu
    }

    public static class BotFeedbackOccasionExtensions
    {
        public static string ToFriendlyString(this BotFeedbackOccasion occasion)
        {
            switch (occasion)
            {
                case BotFeedbackOccasion.Student_Exercise:
                    return "Student Exercise";
                case BotFeedbackOccasion.Student_Exam:
                    return "Student Exam";
                case BotFeedbackOccasion.Student_Schedule:
                    return "Student Schedule";
                case BotFeedbackOccasion.Persistent_Menu:
                    return "Persistent Menu";
                default:
                    return string.Empty;
            }
        }
    }
}
