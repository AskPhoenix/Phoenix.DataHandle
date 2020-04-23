using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IStudentExercise
    {
        IUser Student { get; }
        IExercise Exercise { get; }
        decimal? Grade { get; set; }
    }
}