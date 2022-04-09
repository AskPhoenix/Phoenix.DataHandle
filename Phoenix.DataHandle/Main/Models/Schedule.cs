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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ObviatedAt { get; set; }

        public virtual Classroom? Classroom { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
