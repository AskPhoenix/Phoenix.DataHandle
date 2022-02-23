using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExam
    {
        ILecture Lecture { get; }
        string? Name { get; }
        string? Comments { get; }

        IEnumerable<IGrade> Grades { get; }
        IEnumerable<IMaterial> Materials { get; }
    }
}