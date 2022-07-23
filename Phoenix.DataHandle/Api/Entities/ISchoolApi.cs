using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface ISchoolApi : ISchoolBase
    {
        ISchoolSettingApi SchoolSetting { get; }
    }
}
