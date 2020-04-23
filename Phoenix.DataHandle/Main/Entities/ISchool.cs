using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchool
    {
        string Name { get; set; }
        string Slug { get; set; }
        string City { get; set; }
        string AddressLine { get; set; }
        string Info { get; set; }
        string Endpoint { get; set; }
        string FacebookPageId { get; set; }

        IEnumerable<IClassroom> Classrooms { get; }
        IEnumerable<ICourse> Courses { get; }
    }
}