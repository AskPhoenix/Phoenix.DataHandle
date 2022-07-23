using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchoolConnection : ISchoolConnectionBase
    {
        ISchool Tenant { get; }
    }
}
