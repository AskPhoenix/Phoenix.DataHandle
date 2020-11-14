using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExercise
    {
        ILecture Lecture { get; }
        IBook Book { get; }
        string Name { get; set; }
        string Page { get; set; }
        string Comments { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }

        IEnumerable<IStudentExercise> StudentExercises { get; }
    }
}