using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IOneTimeCode : IOneTimeCodeBase
    {
        IUser User { get; }
    }
}
