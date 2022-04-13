using Phoenix.DataHandle.Main.Types;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IChannel
    {
        ChannelProvider Provider { get; }
        string ProviderName { get; }
        
        IEnumerable<ISchoolLogin> SchoolLogins { get; }
        IEnumerable<IUserLogin> UserLogins { get; }
    }
}
