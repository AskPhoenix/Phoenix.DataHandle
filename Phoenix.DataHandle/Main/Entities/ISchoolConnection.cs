﻿using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchoolConnection : ISchoolConnectionBase
    {
        ISchool Tenant { get; }
    }
}
