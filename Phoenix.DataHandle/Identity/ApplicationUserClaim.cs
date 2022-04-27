using Microsoft.AspNetCore.Identity;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
        public virtual ApplicationUser User { get; set; } = null!;
    }
}