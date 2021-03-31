using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
