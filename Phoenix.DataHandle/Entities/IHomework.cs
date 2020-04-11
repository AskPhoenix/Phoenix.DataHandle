using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface IHomework
    {
        ILecture Lecture { get; set; }
        IEnumerable<IExercise> Exercises { get; set; }
    }
}