using Microsoft.AspNetCore.Identity;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<int>
    {
        public virtual ApplicationUser User { get; set; } = null!;
    }
}