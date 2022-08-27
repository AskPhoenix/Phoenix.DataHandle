using Microsoft.AspNetCore.Identity;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUserToken : IdentityUserToken<int>
    {
        public virtual ApplicationUser User { get; set; } = null!;
    }
}