using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetUsers
    {
        string UserName { get; set; }
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        string FacebookId { get; set; }
        DateTime RegisteredAt { get; }
        int CreatedApplicationType { get; set; }

        IUser User { get; }
        IEnumerable<IAspNetUserRoles> Roles { get; }
    }
}
