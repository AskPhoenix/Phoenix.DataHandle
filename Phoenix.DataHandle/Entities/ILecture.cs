using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface ILecture
    {
        ICourse Course { get; }
        //ITeacher Teacher { get; set; }

        //string Classroom { get; set; }
        DateTime startDateTime { get; set; }
        DateTime endDateTime { get; set; }
        int status { get; set; }
        //IEnumerable<IHomework> Homeworks { get; set; }
    }
}