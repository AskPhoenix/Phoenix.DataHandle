using System;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetUserLogins
    {
        string LoginProvider { get; set; }
        string ProviderKey { get; set; }
        string ProviderDisplayName { get; set; }
        bool IsActive { get; set; }
        int UserId { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }

        IAspNetUsers User { get; }
    }
}
