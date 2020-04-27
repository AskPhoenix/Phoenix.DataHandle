using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class BotFeedback
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Occasion { get; set; }
        public string Category { get; set; }
        public byte? Rating { get; set; }
        public string Comment { get; set; }

        public virtual User Author { get; set; }
    }
}
