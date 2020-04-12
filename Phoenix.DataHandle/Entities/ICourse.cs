using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface ICourse
    {
        ISchool School { get; }
        string name { get; set; }
        IEnumerable<ILecture> Lectures { get;  }
        //IEnumerable<IStudent_Course> Student_Course { get; set; }
        string level { get; set; }
        string group { get; set; }
    }
}