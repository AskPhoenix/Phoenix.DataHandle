using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Exercise
    {
        public Exercise()
        {
            StudentExercise = new HashSet<StudentExercise>();
        }

        public int Id { get; set; }
        public int BookId { get; set; }
        public int LectureId { get; set; }
        public string Name { get; set; }
        public string Page { get; set; }
        public string Info { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Book Book { get; set; }
        public virtual Lecture Lecture { get; set; }
        public virtual ICollection<StudentExercise> StudentExercise { get; set; }
    }
}
