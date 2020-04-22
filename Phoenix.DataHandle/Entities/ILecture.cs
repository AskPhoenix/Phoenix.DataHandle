using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface ILecture
    {
        ICourse Course { get; }
        //ITeacher Teacher { get; set; }

        //string Classroom { get; set; }
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        int Status { get; set; }
        //IEnumerable<IHomework> Homeworks { get; set; }
    }
}