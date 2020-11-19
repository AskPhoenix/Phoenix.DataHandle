using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class BotFeedback
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public BotFeedbackOccasion Occasion { get; set; }
        public BotFeedbackCategory? Category { get; set; }
        public short? Rating { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual AspNetUsers Author { get; set; }
    }
}
