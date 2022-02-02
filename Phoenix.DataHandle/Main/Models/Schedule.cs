using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Lecture = new HashSet<Lecture>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int? ClassroomId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Info { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? ObviatedAt { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Lecture> Lecture { get; set; }
    }
}
