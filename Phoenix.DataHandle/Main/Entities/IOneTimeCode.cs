using Phoenix.DataHandle.Main.Types;
using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IOneTimeCode
    {
        string Token { get; }
        OneTimeCodePurpose Purpose { get; }
        DateTime ExpiresAt { get; }

        IEnumerable<IUserInfo> UserInfos { get; }
        IEnumerable<IUserLogin> UserLogins { get; }
    }
}
