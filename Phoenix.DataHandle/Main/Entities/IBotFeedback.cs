using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBotFeedback
    {
        IUser Author { get; }
        bool AskTriggered { get; }
        BotFeedbackCategory Category { get; }
        short? Rating { get; }
        string? Comment { get; }
    }
}
