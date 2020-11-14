using System;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetUserLogins
    {
        string LoginProvider { get; set; }
        string ProviderKey { get; set; }
        string ProviderDisplayName { get; set; }
        int UserId { get; set; }
        string OneTimeCode { get; set; }
        bool OTCUsed { get; set; }
        DateTimeOffset? OTCCreatedAt { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }

        IAspNetUsers User { get; }
    }
}
