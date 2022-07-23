using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IGrade : IGradeBase
    {
        IUser Student { get; }
        ICourse? Course { get; }
        IExam? Exam { get; }
        IExercise? Exercise { get; }
    }
}
