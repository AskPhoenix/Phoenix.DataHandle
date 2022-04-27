﻿using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ICourse
    {
        short Code { get; }
        ISchool School { get; }
        string Name { get; }
        string? SubCourse { get; }
        string Level { get; }
        string Group { get; }
        string? Comments { get; }
        DateTime FirstDate { get; }
        DateTime LastDate { get; }

        IEnumerable<IGrade> Grades { get; }
        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<ISchedule> Schedules { get; }

        IEnumerable<IBook> Books { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IUserInfo> Users { get; }
    }
}