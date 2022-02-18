using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ILecture
    {
        ICourse Course { get; }
        IClassroom? Classroom { get; }
        ISchedule? Schedule { get; }
        DateTimeOffset StartDateTime { get; set; }
        DateTimeOffset EndDateTime { get; set; }
        LectureStatus Status { get; set; }
        string? OnlineMeetingLink { get; set; }
        string? Comments { get; set; }
        LectureCreatedBy CreatedBy { get; set; }

        IEnumerable<IExam> Exams { get; }
        IEnumerable<IExercise> Exercises { get; }

        IEnumerable<IAspNetUser> Attendees { get; }
    }
}