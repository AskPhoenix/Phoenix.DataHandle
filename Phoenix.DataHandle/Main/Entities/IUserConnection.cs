using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUserConnection : IUserConnectionBase
    {
        IUser Tenant { get; }
    }
}
