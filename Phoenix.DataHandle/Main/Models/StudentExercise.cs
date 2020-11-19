using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class StudentExercise
    {
        public int StudentId { get; set; }
        public int ExerciseId { get; set; }
        public decimal? Grade { get; set; }

        public virtual Exercise Exercise { get; set; }
        public virtual AspNetUsers Student { get; set; }
    }
}
