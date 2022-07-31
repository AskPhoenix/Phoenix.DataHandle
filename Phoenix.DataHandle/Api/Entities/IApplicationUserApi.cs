using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface IApplicationUserApi : IApplicationUserBase
    {
        IUserApi User { get; }
    }
}
