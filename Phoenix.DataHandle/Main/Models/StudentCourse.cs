using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual AspNetUsers Student { get; set; }
    }
}
