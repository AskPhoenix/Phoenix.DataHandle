using System;
namespace Phoenix.DataHandle.Entities
{
    public interface IAspNetRoles
    {
        string Name { get; set; }
        string NormalizedName { get; set; }
        Role Role { get; }
    }
}
