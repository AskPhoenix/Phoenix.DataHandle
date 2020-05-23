using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Lecture
    {
        public Lecture()
        {
            Attendance = new HashSet<Attendance>();
            Homework = new HashSet<Homework>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int ClassroomId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public LectureStatus Status { get; set; }
        public string Info { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<Homework> Homework { get; set; }
    }
}
