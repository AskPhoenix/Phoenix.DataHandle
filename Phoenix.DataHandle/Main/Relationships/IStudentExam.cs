using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IStudentExam
    {
        IUser Student { get; }
        IExam Exam { get; }
        decimal? Grade { get; set; }
    }
}