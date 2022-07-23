using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Base.Entities
{
    public interface IBotFeedbackBase
    {
        bool AskTriggered { get; }
        BotFeedbackCategory Category { get; }
        short? Rating { get; }
        string? Comment { get; }
    }
}
