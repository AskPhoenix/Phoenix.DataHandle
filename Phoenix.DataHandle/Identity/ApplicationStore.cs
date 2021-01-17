using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>
    {
        public ApplicationStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }

        public Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = new CancellationToken())
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(phoneNumber));

            return this.Context.Users.SingleOrDefaultAsync(applicatonUser => applicatonUser.PhoneNumber == phoneNumber, cancellationToken);
        }

        public Task<ApplicationUser> FindByProviderKeyAsync(LoginProvider provider, string key, CancellationToken cancellationToken = new CancellationToken())
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));

            string providerName = provider.GetProviderName();
            return this.Context.Users.
                Include(u => u.ApplicationUserLogin).
                SingleOrDefaultAsync(u => u.ApplicationUserLogin.Any(l => l.LoginProvider == providerName && l.ProviderKey == key), cancellationToken);

        }
    }
}