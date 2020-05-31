using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ILecture
    {
        ICourse Course { get; }
        IClassroom Classroom { get; }
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        LectureStatus Status { get; set; }
        string Info { get; set; }

        IEnumerable<IAttendance> Attendances { get; }
        IEnumerable<IExercise> Exercises { get; }
    }
}