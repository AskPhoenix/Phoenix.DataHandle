using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetRole
    {
        Role Type { get; }
        string? Name { get; }

        IEnumerable<IAspNetUser> Users { get; }
    }
}
