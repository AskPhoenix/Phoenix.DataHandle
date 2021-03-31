using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
