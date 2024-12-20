﻿using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBroadcast : IBroadcastBase
    {
        ISchool School { get; }
        IUser? Author { get; }
        ICourse? Course { get; }
    }
}
