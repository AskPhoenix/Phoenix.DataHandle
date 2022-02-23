using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExercise
    {
        ILecture Lecture { get; }
        string Name { get; set; }
        IBook? Book { get; }
        string? Page { get; set; }
        string? Comments { get; set; }

        IEnumerable<IGrade> Grades { get; }
    }
}