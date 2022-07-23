using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IExerciseApi : IExerciseBase
    {
        int LectureId { get; }
        int? BookId { get; }
    }
}
