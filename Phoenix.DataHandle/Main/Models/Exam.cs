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
        public int LectureId { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Lecture Lecture { get; set; }
        public virtual ICollection<Material> Material { get; set; }
        public virtual ICollection<StudentExam> StudentExam { get; set; }
    }
}
