using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface ICourseApi : ICourseBase
    {
        int SchoolId { get; }
    }
}
