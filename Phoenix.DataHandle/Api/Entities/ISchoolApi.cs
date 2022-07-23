using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface ISchoolApi : ISchoolBase
    {
        ISchoolSettingApi SchoolSetting { get; }
    }
}
