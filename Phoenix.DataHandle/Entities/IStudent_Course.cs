using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IStudent_Course
    {
        IStudent Student { get; set; }
        ICourse Course { get; set; }
        float? Grade { get; set; }
    }
}