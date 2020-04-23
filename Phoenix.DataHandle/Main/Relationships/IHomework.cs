using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IHomework
    {
        ILecture ForLecture { get; }
        IExercise Exercise { get; }
    }
}