using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IClassroomApi : IClassroomBase
    {
        int SchoolId { get; }
    }
}
