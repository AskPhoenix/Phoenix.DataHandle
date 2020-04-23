using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetRoles
    {
        Role Type { get; set; }
        string Name { get; set; }
        string NormalizedName { get; set; }
        
        IEnumerable<IAspNetUserRoles> AspNetUserRoles { get; }
    }
}
