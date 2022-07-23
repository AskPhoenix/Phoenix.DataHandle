using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface ICourseApi : ICourseBase
    {
        int SchoolId { get; }
    }
}
