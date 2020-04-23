using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class CourseBook
    {
        public int CourseId { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Course Course { get; set; }
    }
}
