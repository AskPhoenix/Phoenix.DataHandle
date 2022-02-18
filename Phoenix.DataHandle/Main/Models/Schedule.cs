using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int? ClassroomId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string? Comments { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? ObviatedAt { get; set; }

        public virtual Classroom? Classroom { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
