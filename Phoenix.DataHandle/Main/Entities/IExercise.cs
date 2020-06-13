using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExercise
    {
        IBook Book { get; }
        string Name { get; set; }
        string Page { get; set; }
        string Comments { get; set; }

        ILecture Lecture { get; }
        IEnumerable<IStudentExercise> StudentExercises { get; }
    }
}