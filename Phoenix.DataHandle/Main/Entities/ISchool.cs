using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchool
    {
        string Name { get; set; }
        string Slug { get; set; }
        string City { get; set; }
        string AddressLine { get; set; }
        string? Description { get; set; }
        
        ISchoolInfo SchoolInfo { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IClassroom> Classrooms { get; }
        IEnumerable<ICourse> Courses { get; }
        IEnumerable<ISchoolLogin> SchoolLogins { get; }

        IEnumerable<IAspNetUser> Users { get; }
    }
}