using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IAspNetUserApi : IAspNetUserBase
    {
        IUserApi User { get; }
    }
}
