using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ILecture
    {
        DateTimeOffset StartDateTime { get; set; }
        DateTimeOffset EndDateTime { get; set; }
        LectureStatus Status { get; set; }
        string Info { get; set; }
        string OnlineMeetingLink { get; set; }
        LectureCreatedBy CreatedBy { get; set; }

        ICourse Course { get; }
        IClassroom Classroom { get; }
        ISchedule Schedule { get; }
        IExam Exam { get; }

        IEnumerable<IAttendance> Attendances { get; }
        IEnumerable<IExercise> Exercises { get; }
    }
}