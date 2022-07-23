using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchoolSetting : ISchoolSettingBase
    {
        ISchool School { get; }
    }
}
