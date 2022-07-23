using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUserConnection : IUserConnectionBase
    {
        IUser Tenant { get; }
    }
}
