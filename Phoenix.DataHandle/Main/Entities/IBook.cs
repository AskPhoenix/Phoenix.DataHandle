using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBook : IBookBase
    {
        IEnumerable<IExercise> Exercises { get; }
        IEnumerable<IMaterial> Materials { get; }
        
        IEnumerable<ICourse> Courses { get; }
    }
}