using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBook
    {
        string Name { get; set; }
        string Publisher { get; set; }
        string Info { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }

        IEnumerable<ICourseBook> CourseBooks { get; }
        IEnumerable<IExercise> Exercises { get; }
        IEnumerable<IMaterial> Materials { get; }
    }
}