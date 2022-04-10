using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ILecture
    {
        ICourse Course { get; }
        IClassroom? Classroom { get; }
        ISchedule? Schedule { get; }
        DateTimeOffset StartDateTime { get; }
        DateTimeOffset EndDateTime { get; }
        string? OnlineMeetingLink { get; }
        bool AttendancesNoted { get; }
        string? Comments { get; }

        IEnumerable<IExam> Exams { get; }
        IEnumerable<IExercise> Exercises { get; }

        IEnumerable<IAspNetUser> Attendees { get; }
    }
}