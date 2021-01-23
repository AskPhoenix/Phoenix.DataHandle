using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class TeacherCourse
    {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual AspNetUsers Teacher { get; set; }
    }
}
