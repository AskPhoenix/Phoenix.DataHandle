using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IOneTimeCode
    {
        string Token { get; }
        OneTimeCodePurpose Purpose { get; }
        DateTime ExpiresAt { get; }

        IUser User { get; }
    }
}
