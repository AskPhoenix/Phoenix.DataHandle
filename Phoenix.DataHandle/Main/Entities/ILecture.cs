using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ILecture
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        LectureStatus Status { get; set; }
        string Info { get; set; }
        LectureCreatedBy CreatedBy { get; set; }

        ICourse Course { get; }
        IClassroom Classroom { get; }
        ISchedule Schedule { get; }
        IExam Exam { get; }

        IEnumerable<IAttendance> Attendances { get; }
        IEnumerable<IExercise> Exercises { get; }
    }
}