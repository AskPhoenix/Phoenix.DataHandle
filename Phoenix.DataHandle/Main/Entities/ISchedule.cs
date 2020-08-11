using System;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchedule
    {
        DayOfWeek DayOfWeek { get; set; }
        DateTimeOffset StartTime { get; set; }
        DateTimeOffset EndTime { get; set; }
        string Info { get; set; }

        ICourse Course { get; }
        IClassroom Classroom { get; }
    }
}