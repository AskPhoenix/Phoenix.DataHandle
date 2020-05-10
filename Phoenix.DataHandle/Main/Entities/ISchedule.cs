using System;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchedule
    {
        DayOfWeek dayOfWeek { get; set; }
        DateTime startAt { get; set; }
        DateTime endAt { get; set; }

        ICourse Course { get; }
        IClassroom Classroom { get; }
    }
}