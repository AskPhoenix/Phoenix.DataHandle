using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExam
    {
        ILecture Lecture { get; }
        string Comments { get; set; }

        IEnumerable<IMaterial> Materials { get; }
        IEnumerable<IStudentExam> StudentExams { get; }
    }
}