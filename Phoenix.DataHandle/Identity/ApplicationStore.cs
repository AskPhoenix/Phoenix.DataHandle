using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>
    {
        public ApplicationStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        public Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = new CancellationToken())
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(phoneNumber));

            return this.Context.Users.FirstOrDefaultAsync(applicatonUser => string.Equals(applicatonUser.PhoneNumber, phoneNumber, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
        }

        public Task<ApplicationUser> FindByFacebookIdAsync(string facebookId, CancellationToken cancellationToken = new CancellationToken())
        {
            if (string.IsNullOrWhiteSpace(facebookId))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(facebookId));

            return this.Context.Users.FirstOrDefaultAsync(applicatonUser => string.Equals(applicatonUser.FacebookId, facebookId, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
        }
    }
}