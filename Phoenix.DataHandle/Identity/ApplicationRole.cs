using Microsoft.AspNetCore.Identity;
using Phoenix.DataHandle.Main.Types;
using Phoenix.DataHandle.Utilities;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
            : base()
        {
            Users = new HashSet<ApplicationUser>();
            Claims = new HashSet<ApplicationRoleClaim>();
        }

        public RoleRank Rank => this.NormalizedName.ToRoleRank();

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<ApplicationRoleClaim> Claims { get; set; }

        public void Normalize()
        {
            this.NormalizedName = this.Name.ToTitleCase().Remove(' ');
        }
    }
}