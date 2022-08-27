using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.DataEntry.Entities
{
    public interface ISchoolAcf : ISchoolBase, ISchoolSettingBase
    {
        ISchoolSettingBase SchoolSetting { get; }
    }
}
