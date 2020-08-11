using System;
using System.Collections.Generic;

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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Lecture> Lecture { get; set; }
    }
}
