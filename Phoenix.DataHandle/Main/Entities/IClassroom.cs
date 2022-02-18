using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IClassroom
    {
        ISchool School { get; }
        string Name { get; set; }
        string? Comments { get; set; }

        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<ISchedule> Schedules { get; }
    }
}
