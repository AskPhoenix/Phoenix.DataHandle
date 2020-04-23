using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExam
    {
        ICourse Course { get; }
        IClassroom Classroom { get; }
        DateTime StartsAt { get; set; }
        DateTime EndsAt { get; set; }
        string Comments { get; set; }

        IEnumerable<IMaterial> Materials { get; }
        IEnumerable<IStudentExam> StudentExams { get; }
    }
}