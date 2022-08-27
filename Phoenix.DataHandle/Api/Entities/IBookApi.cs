using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IBookApi : IBookBase
    {
        int SchoolId { get; }
    }
}
