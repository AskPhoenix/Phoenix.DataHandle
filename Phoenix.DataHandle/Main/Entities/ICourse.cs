using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ICourse : ICourseBase
    {
        ISchool School { get; }

        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IGrade> Grades { get; }
        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<ISchedule> Schedules { get; }

        IEnumerable<IBook> Books { get; }
        IEnumerable<IUser> Users { get; }
    }
}