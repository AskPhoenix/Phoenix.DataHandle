using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Exam
    {
        public Exam()
        {
            Material = new HashSet<Material>();
            StudentExam = new HashSet<StudentExam>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int ClassroomId { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public string Comments { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Material> Material { get; set; }
        public virtual ICollection<StudentExam> StudentExam { get; set; }
    }
}
