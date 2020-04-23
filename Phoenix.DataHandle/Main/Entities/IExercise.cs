using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExercise
    {
        IBook Book { get; }
        short Page { get; set; }
        string Name { get; set; }

        IEnumerable<IHomework> Homeworks { get; }
        IEnumerable<IStudentExercise> StudentExercises { get; }
    }
}