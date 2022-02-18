namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBotFeedback
    {
        IAspNetUser Author { get; }
        bool AskTriggered { get; set; }
        BotFeedbackType Type { get; set; }
        short? Rating { get; set; }
        string? Comment { get; set; }
    }
}
