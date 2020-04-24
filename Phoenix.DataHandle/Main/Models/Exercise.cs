using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Exercise
    {
        public Exercise()
        {
            Homework = new HashSet<Homework>();
            StudentExercise = new HashSet<StudentExercise>();
        }

        public int Id { get; set; }
        public int BookId { get; set; }
        public short Page { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public virtual Book Book { get; set; }
        public virtual ICollection<Homework> Homework { get; set; }
        public virtual ICollection<StudentExercise> StudentExercise { get; set; }
    }
}
