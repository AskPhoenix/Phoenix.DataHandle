using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Base
{
    public interface IOneTimeCodeBase
    {
        string Token { get; }
        OneTimeCodePurpose Purpose { get; }
        DateTime ExpiresAt { get; }
    }
}
