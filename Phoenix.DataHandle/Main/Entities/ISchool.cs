using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchool
    {
        string Name { get; }
        string Slug { get; }
        string City { get; }
        string AddressLine { get; }
        string? Description { get; }
        
        ISchoolInfo SchoolInfo { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IClassroom> Classrooms { get; }
        IEnumerable<ICourse> Courses { get; }
        IEnumerable<ISchoolLogin> SchoolLogins { get; }

        IEnumerable<IAspNetUser> Users { get; }
    }
}