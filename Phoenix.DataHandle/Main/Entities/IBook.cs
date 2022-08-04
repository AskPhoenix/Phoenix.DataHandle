using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBook : IBookBase
    {
        ISchool School { get; }
        IEnumerable<IExercise> Exercises { get; }
        IEnumerable<IMaterial> Materials { get; }
        
        IEnumerable<ICourse> Courses { get; }
    }
}