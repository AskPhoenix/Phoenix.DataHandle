using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchedule
    {
        ICourse Course { get; }
        IClassroom? Classroom { get; }
        DayOfWeek DayOfWeek { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        string? Comments { get; }

        IEnumerable<ILecture> Lectures { get; }
    }
}