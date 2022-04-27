using Microsoft.AspNetCore.Identity;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<int>
    {
        public virtual ApplicationRole Role { get; set; } = null!;
    }
}