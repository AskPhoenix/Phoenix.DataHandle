using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchool
    {
        int Code { get; }
        string Name { get; }
        string Slug { get; }
        string City { get; }
        string AddressLine { get; }
        string? Description { get; }
        
        ISchoolSetting SchoolSetting { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IClassroom> Classrooms { get; }
        IEnumerable<ICourse> Courses { get; }
        IEnumerable<ISchoolConnection> SchoolConnections { get; }

        IEnumerable<IUser> Users { get; }
    }
}