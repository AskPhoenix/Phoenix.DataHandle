using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual Lecture Lecture { get; set; }
        public virtual ICollection<Material> Material { get; set; }
        public virtual ICollection<StudentExam> StudentExam { get; set; }
    }
}
