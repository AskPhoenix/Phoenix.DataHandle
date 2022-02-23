using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IChannel
    {
        int Code { get; }
        string Provider { get; }

        IEnumerable<IAspNetUserLogin> AspNetUserLogins { get; }
        IEnumerable<ISchoolLogin> SchoolLogins { get; }
    }
}
