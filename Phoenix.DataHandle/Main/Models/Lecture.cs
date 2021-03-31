using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Lecture
    {
        public Lecture()
        {
            Attendance = new HashSet<Attendance>();
            Exercise = new HashSet<Exercise>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int? ClassroomId { get; set; }
        public int? ScheduleId { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public LectureStatus Status { get; set; }
        public string OnlineMeetingLink { get; set; }
        public string Info { get; set; }
        public LectureCreatedBy CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Course Course { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<Exercise> Exercise { get; set; }
    }
}
