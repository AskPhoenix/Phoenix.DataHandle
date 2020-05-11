using System;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchedule
    {
        DayOfWeek DayOfWeek { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Info { get; set; }

        ICourse Course { get; }
        IClassroom Classroom { get; }
    }
}