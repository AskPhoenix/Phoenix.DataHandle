using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExercise
    {
        IBook Book { get; }
        string Page { get; set; }
        string Name { get; set; }
        string Info { get; set; }

        ILecture Lecture { get; }
        IEnumerable<IStudentExercise> StudentExercises { get; }
    }
}