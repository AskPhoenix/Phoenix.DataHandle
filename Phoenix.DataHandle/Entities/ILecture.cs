using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface ILecture
    {
        ICourse Course { get; set; }
        ITeacher Teacher { get; set; }

        string Classroom { get; set; }
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        bool isCanceled { get; set; }
        IEnumerable<IHomework> Homeworks { get; set; }
    }
}