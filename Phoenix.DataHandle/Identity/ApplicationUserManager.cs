using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        protected internal new ApplicationStore Store => (ApplicationStore)base.Store;

        public ApplicationUserManager(
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<ApplicationUserManager> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators,
                  passwordValidators, new UpperInvariantLookupNormalizer(),
                  errors, services, logger)
        {
        }

        #region Find

        public Task<ApplicationUser?> FindByPhoneNumberAsync(string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            return this.Store.FindByPhoneNumberAsync(phoneNumber, cancellationToken);
        }

        public Task<ApplicationUser?> FindByProviderKeyAsync(string provider, string key,
            CancellationToken cancellationToken = default)
        {
            return this.Store.FindByProviderKeyAsync(provider, key, cancellationToken);
        }

        #endregion

        #region Get Roles

        public async Task<IList<RoleRank>> GetRoleRanksAsync(ApplicationUser user)
        {
            return (await this.GetRolesAsync(user)).Select(rn => rn.ToRoleRank()).ToList();
        }

        public async Task<IList<string>> GetNormRolesAsync(ApplicationUser user)
        {
            return (await this.GetRoleRanksAsync(user)).Select(rr => rr.ToNormalizedString()).ToList();
        }

        #endregion

        #region Remove From Roles

        public async Task<IdentityResult> RemoveFromAllButOneRoleAsync(ApplicationUser user, string role)
        {
            var roles = await this.GetNormRolesAsync(user);

            return await this.RemoveFromRolesAsync(user,
                roles.Where(rn => !string.Equals(rn, role, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task<IdentityResult> RemoveFromAllButClientRolesAsync(ApplicationUser user)
        {
            var nonClientRoles = (await this.GetRoleRanksAsync(user))
                .Where(rr => !rr.IsClient())
                .Select(rr => rr.ToNormalizedString());

            return await this.RemoveFromRolesAsync(user, nonClientRoles);
        }

        public async Task<IdentityResult> RemoveFromAllButPersonnelRolesAsync(ApplicationUser user)
        {
            var nonPersonnelRoles = (await this.GetRoleRanksAsync(user))
                .Where(rr => !rr.IsStaffOrBackend())
                .Select(rr => rr.ToNormalizedString());

            return await this.RemoveFromRolesAsync(user, nonPersonnelRoles);
        }

        #endregion
    }
}