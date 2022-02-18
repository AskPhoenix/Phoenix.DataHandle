using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Lecture
    {
        public Lecture()
        {
            Exams = new HashSet<Exam>();
            Exercises = new HashSet<Exercise>();
            Attendees = new HashSet<AspNetUser>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int? ClassroomId { get; set; }
        public int? ScheduleId { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public LectureStatus Status { get; set; }
        public string? OnlineMeetingLink { get; set; }
        public bool AttendancesNoted { get; set; }
        public string? Comments { get; set; }
        public LectureCreatedBy CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual Classroom? Classroom { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual Schedule? Schedule { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }

        public virtual ICollection<AspNetUser> Attendees { get; set; }
    }
}
