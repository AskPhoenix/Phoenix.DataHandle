using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ICourse
    {
        short Code { get; set; }
        ISchool School { get; }
        string Name { get; set; }
        string? SubCourse { get; set; }
        string Level { get; set; }
        string Group { get; set; }
        string? Comments { get; set; }
        DateTimeOffset FirstDate { get; set; }
        DateTimeOffset LastDate { get; set; }

        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<ISchedule> Schedules { get; }

        IEnumerable<IBook> Books { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IAspNetUser> Teachers { get; }
    }
}