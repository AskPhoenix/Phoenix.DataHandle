using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IClassroomApi : IClassroomBase
    {
        int SchoolId { get; }
    }
}
