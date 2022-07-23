using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface ISchoolSettingApi : ISchoolSettingBase
    {
        int SchoolId { get; }
    }
}
