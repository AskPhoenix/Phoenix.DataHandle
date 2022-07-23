using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IScheduleApi : IScheduleBase
    {
        int CourseId { get; }
        int? ClassroomId { get; }
    }
}