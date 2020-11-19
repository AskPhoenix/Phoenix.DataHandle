using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IStudentExam
    {
        IAspNetUsers Student { get; }
        IExam Exam { get; }
        decimal? Grade { get; set; }
    }
}