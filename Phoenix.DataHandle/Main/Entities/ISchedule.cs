using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchedule
    {
        ICourse Course { get; }
        IClassroom? Classroom { get; }
        DayOfWeek DayOfWeek { get; set; }
        DateTimeOffset StartTime { get; set; }
        DateTimeOffset EndTime { get; set; }
        string? Comments { get; set; }

        IEnumerable<ILecture> Lectures { get; }
    }
}