using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExercise : IExerciseBase
    {
        ILecture Lecture { get; }
        IBook? Book { get; }
        
        IEnumerable<IGrade> Grades { get; }
    }
}