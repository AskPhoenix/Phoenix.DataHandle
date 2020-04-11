using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface ITeacher : IPerson
    {
        IEnumerable<IStudent_Course> Student_Course { get; set; }

    }
}