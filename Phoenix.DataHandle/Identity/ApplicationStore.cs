using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationStore : UserStore
        <ApplicationUser,
        ApplicationRole,
        ApplicationContext,
        int,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationUserToken,
        ApplicationRoleClaim>
    {
        public ApplicationStore(ApplicationContext context,
            IdentityErrorDescriber describer = default!)
            : base(context, describer)
        {
        }

        #region Find

        public Task<ApplicationUser?> FindByPhoneNumberAsync(string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            return Users.SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber,
                cancellationToken);
        }

        public Task<ApplicationUser?> FindByProviderKeyAsync(string provider, string key,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(provider))
                throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            return Users
                .Include(u => u.Logins)
                .SingleOrDefaultAsync(
                    u => u.Logins.Any(l => l.LoginProvider == provider && l.ProviderKey == key),
                cancellationToken);
        }

        #endregion
    }
}