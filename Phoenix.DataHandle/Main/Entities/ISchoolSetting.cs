﻿using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchoolSetting : ISchoolSettingBase
    {
        ISchool School { get; }
    }
}
