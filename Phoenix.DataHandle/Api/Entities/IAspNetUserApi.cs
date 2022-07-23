using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IAspNetUserApi : IAspNetUserBase
    {
        IUserApi User { get; }
    }
}
