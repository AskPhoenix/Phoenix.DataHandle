using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IDevRegistration : IDevRegistrationBase
    {
        IUser? Developer { get; }
    }
}
