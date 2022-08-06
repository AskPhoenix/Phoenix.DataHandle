namespace Phoenix.DataHandle.Main.Models
{
    public partial class BotFeedback
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public bool AskTriggered { get; set; }
        public Types.BotFeedbackCategory Category { get; set; }
        public short? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User? Author { get; set; }
    }
}
