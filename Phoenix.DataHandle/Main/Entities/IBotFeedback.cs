namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBotFeedback
    {
        IAspNetUser Author { get; }
        bool AskTriggered { get; }
        BotFeedbackType Type { get; }
        short? Rating { get; }
        string? Comment { get; }
    }
}
