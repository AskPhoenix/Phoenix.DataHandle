using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class BotFeedback
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Topic { get; set; }
        public byte? Rating { get; set; }
        public string Comments { get; set; }

        public virtual User Author { get; set; }
    }
}
