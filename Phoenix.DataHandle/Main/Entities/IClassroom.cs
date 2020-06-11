using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IClassroom
    {
        ISchool School { get; }
        string Name { get; set; }
        string Info { get; set; }

        IEnumerable<ILecture> Lectures { get; }
    }
}
