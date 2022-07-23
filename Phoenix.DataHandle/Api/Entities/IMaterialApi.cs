using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IMaterialApi : IMaterialBase
    {
        int ExamId { get; }
        int? BookId { get; }
    }
}
