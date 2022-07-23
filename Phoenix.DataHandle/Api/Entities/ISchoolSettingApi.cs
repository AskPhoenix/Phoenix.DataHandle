using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface ISchoolSettingApi : ISchoolSettingBase
    {
        int SchoolId { get; }
    }
}
