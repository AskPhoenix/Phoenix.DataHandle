using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IAspNetUserRoles
    {
        IAspNetUsers User { get; }
        IAspNetRoles Role { get; }
    }
}