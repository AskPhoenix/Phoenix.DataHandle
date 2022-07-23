using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IGradeApi : IGradeBase
    {
        int StudentId { get; }
        int? CourseId { get; }
        int? ExamId { get; }
        int? ExerciseId { get; }
    }
}
