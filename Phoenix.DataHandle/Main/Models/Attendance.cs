using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
