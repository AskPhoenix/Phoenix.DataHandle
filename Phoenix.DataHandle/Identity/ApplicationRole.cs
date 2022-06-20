using Microsoft.AspNetCore.Identity;
using Phoenix.DataHandle.Main.Types;
using Phoenix.DataHandle.Utilities;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
            : base()
        {
            UserRoles = new HashSet<ApplicationUserRole>();
            Claims = new HashSet<ApplicationRoleClaim>();
        }

        public RoleRank Rank => this.NormalizedName.ToRoleRank();

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> Claims { get; set; }

        public void Normalize()
        {
            this.NormalizedName = this.Name.ToTitleCase().Remove(' ');
        }
    }
}