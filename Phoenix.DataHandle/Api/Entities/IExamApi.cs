using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IExamApi : IExamBase
    {
        int LectureId { get; }
    }
}
