﻿using System;
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
        public int LectureId { get; set; }
        public string Name { get; set; }
        public int? BookId { get; set; }
        public string Page { get; set; }
        public string Comments { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual Book Book { get; set; }
        public virtual Lecture Lecture { get; set; }
        public virtual ICollection<StudentExercise> StudentExercise { get; set; }
    }
}
