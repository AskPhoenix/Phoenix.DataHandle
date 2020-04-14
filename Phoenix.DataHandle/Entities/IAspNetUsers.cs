using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IAspNetUser
    {
        string UserName { get; set; }
        string NormalizedUserName { get; set; }
        string Email { get; set; }
        string NormalizedEmail { get; set; }
        bool EmailConfirmed { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        DateTime created_at { get; set; }
        DateTime? updated_at { get; set; }

        IUser User { get; }
    }
}
