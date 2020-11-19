using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Attendance
    {
        public int StudentId { get; set; }
        public int LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }
        public virtual AspNetUsers Student { get; set; }
    }
}
