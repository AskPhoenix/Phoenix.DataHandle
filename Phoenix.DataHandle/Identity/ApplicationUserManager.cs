using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Phoenix.DataHandle.Models;
using Talagozis.AspNetCore.Models;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>, IUserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<ApplicationUserManager> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            user.created_at = DateTime.Now;
            if (user.User == null)
                user.User = new User();

            return base.CreateAsync(user);
        }

        public Task<IdentityResult> CreateAsync<TIdentity>(TIdentity user, string password) where TIdentity : IdentityUser<int>
        {
            ApplicationUser applicationUser = user as ApplicationUser ?? new ApplicationUser(user);

            applicationUser.created_at = DateTime.Now;
            if (applicationUser.User == null)
                applicationUser.User = new User();

            return base.CreateAsync(applicationUser, password);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync<TIdentity>(TIdentity user) where TIdentity : IdentityUser<int>
        {
            ApplicationUser applicationUser = user as ApplicationUser ?? new ApplicationUser(user);

            return base.GenerateEmailConfirmationTokenAsync(applicationUser);
        }
    }
}