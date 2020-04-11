using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface ICourse
    {
        ISchool School { get; set; }
        IEnumerable<ILecture> Lectures { get; set; }
        IEnumerable<IStudent_Course> Student_Course { get; set; }
        string Level { get; set; }
        string Group { get; set; }
    }
}