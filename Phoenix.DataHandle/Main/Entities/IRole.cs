using Phoenix.DataHandle.Main.Types;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IRole
    {
        RoleRank Rank { get; }
        string? Name { get; }

        IEnumerable<IUser> Users { get; }
    }
}
