using System;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBotFeedback
    {
        IUser Author { get; }
        BotFeedbackOccasion Occasion { get; set; }
        BotFeedbackCategory? Category { get; set; }
        short? Rating { get; set; }
        string Comment { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }
}