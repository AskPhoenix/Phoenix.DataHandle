using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchool : ISchoolBase
    {
        ISchoolSetting SchoolSetting { get; }
        IEnumerable<IBook> Books { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IClassroom> Classrooms { get; }
        IEnumerable<ICourse> Courses { get; }
        IEnumerable<ISchoolConnection> SchoolConnections { get; }

        IEnumerable<IUser> Users { get; }
    }
}