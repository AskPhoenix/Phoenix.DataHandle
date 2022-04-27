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
            InverseReplacementLecture = new HashSet<Lecture>();
            Attendees = new HashSet<User>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int? ClassroomId { get; set; }
        public int? ScheduleId { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public string? OnlineMeetingLink { get; set; }
        public Types.LectureOccasion Occasion { get; set; }
        public bool AttendancesNoted { get; set; }
        public bool IsCancelled { get; set; }
        public int? ReplacementLectureId { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ObviatedAt { get; set; }

        public virtual Classroom? Classroom { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual Lecture? ReplacementLecture { get; set; }
        public virtual Schedule? Schedule { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
        public virtual ICollection<Lecture> InverseReplacementLecture { get; set; }

        public virtual ICollection<User> Attendees { get; set; }
    }
}
