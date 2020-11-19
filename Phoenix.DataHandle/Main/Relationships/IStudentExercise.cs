using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IStudentExercise
    {
        IAspNetUsers Student { get; }
        IExercise Exercise { get; }
        decimal? Grade { get; set; }
    }
}