using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface ILectureApi : ILectureBase
    {
        int CourseId { get; }
        int? ClassroomId { get; }
        int? ScheduleId { get; }
        int? ReplacementLectureId { get; }
    }
}
