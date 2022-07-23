using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IScheduleApi : IScheduleBase
    {
        int CourseId { get; }
        int? ClassroomId { get; }
    }
}