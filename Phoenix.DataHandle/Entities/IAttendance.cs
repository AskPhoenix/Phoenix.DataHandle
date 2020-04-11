using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IAttendance
    {
        IStudent Student { get; set; }
        ILecture Lecture { get; set; }
    }
}