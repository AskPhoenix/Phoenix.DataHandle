﻿using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchedule
    {
        ICourse Course { get; }
        IClassroom Classroom { get; }
        short Code { get; set; }
        DayOfWeek DayOfWeek { get; set; }
        DateTimeOffset StartTime { get; set; }
        DateTimeOffset EndTime { get; set; }
        string Info { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }

        IEnumerable<ILecture> Lectures { get; }
    }
}