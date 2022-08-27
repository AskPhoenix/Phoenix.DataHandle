using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Api.Entities
{
    public interface ISchoolConnectionApi : ISchoolConnectionBase
    {
        int TenantId { get; }
    }
}
