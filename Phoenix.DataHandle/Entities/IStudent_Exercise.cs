using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IStudent_Exercise
    {
        IStudent Student { get; set; }
        IExercise Exercise { get; set; }
        float? Grade { get; set; }
    }
}