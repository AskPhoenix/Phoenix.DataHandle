using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IChannel
    {
        int Code { get; set; }
        string Provider { get; set; }

        IEnumerable<IAspNetUserLogin> AspNetUserLogins { get; }
        IEnumerable<ISchoolLogin> SchoolLogins { get; }
    }
}
