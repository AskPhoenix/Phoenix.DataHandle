using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Homework
    {
        public int ForLectureId { get; set; }
        public int ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }
        public virtual Lecture ForLecture { get; set; }
    }
}
