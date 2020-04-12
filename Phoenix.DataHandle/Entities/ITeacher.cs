using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface ITeacher : IUser
    {
        IEnumerable<IStudent_Course> Student_Course { get; set; }

    }
}