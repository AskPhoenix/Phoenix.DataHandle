using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class TeacherCourse
    {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual User Teacher { get; set; }
    }
}
