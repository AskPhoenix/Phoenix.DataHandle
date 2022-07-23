using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IOneTimeCode : IOneTimeCodeBase
    {
        IUser User { get; }
    }
}
