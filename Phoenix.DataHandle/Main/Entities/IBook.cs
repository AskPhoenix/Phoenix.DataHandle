using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBook
    {
        string Name { get; set; }
        string? Publisher { get; set; }
        string? Comments { get; set; }

        IEnumerable<IExercise> Exercises { get; }
        IEnumerable<IMaterial> Materials { get; }
        
        IEnumerable<ICourse> Courses { get; }
    }
}