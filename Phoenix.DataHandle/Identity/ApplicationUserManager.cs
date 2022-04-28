﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        protected internal new ApplicationStore Store => (ApplicationStore)base.Store;

        public ApplicationUserManager(
            ApplicationStore store,
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
    }
}