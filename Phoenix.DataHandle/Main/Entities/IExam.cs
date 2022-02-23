using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExam
    {
        ILecture Lecture { get; }
        string? Name { get; set; }
        string? Comments { get; set; }

        IEnumerable<IGrade> Grades { get; }
        IEnumerable<IMaterial> Materials { get; }
    }
}