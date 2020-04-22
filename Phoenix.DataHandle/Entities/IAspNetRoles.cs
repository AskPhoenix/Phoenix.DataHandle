using System;
namespace Phoenix.DataHandle.Entities
{
    public interface IAspNetRoles
    {
        Role Type { get; }
        string Name { get; set; }
        string NormalizedName { get; set; }
    }
}
