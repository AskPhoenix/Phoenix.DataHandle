﻿using Phoenix.DataHandle.Main.Types;
using System;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IOneTimeCode
    {
        string Token { get; }
        OneTimeCodePurpose Purpose { get; }
        DateTime ExpiresAt { get; }

        IUserInfo User { get; }
    }
}
