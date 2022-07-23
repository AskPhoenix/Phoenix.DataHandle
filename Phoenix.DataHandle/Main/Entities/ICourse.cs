﻿using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ICourse : ICourseBase
    {
        ISchool School { get; }

        IEnumerable<IGrade> Grades { get; }
        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<ISchedule> Schedules { get; }

        IEnumerable<IBook> Books { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IUser> Users { get; }
    }
}