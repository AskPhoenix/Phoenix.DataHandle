using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IExamApi : IExamBase
    {
        int LectureId { get; }
    }
}
