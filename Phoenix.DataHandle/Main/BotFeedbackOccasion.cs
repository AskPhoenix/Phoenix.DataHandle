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
            return occasion switch
            {
                BotFeedbackOccasion.Student_Exercise => "Student Exercise",
                BotFeedbackOccasion.Student_Exam => "Student Exam",
                BotFeedbackOccasion.Student_Schedule => "Student Schedule",
                BotFeedbackOccasion.Persistent_Menu => "Persistent Menu",
                _ => string.Empty,
            };
        }
    }
}
