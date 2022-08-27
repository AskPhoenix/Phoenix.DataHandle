using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IClassroom : IClassroomBase
    {
        ISchool School { get; }

        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<ISchedule> Schedules { get; }
    }
}
