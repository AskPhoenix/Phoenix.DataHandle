using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetRole
    {
        Role Type { get; set; }
        string? Name { get; set; }

        IEnumerable<IAspNetUser> Users { get; }
    }
}
