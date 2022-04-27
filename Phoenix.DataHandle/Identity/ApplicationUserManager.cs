using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        protected internal new ApplicationStore Store =>
            base.Store as ApplicationStore ?? throw new NotSupportedException($"{nameof(base.Store)} is not a {nameof(ApplicationStore)} type.");

        public ApplicationUserManager(
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<ApplicationUserManager> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators,
                  passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (user.UserInfo is null)
                user.UserInfo = new UserInfo();

            //user.UserInfo.CreatedAt = DateTime.Now;

            return base.CreateAsync(user, password);
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (user.UserInfo is null)
                user.UserInfo = new UserInfo();

            //user.UserInfo.CreatedAt = DateTime.Now;

            return base.CreateAsync(user);
        }

        public Task<ApplicationUser?> FindByPhoneNumberAsync(string phoneNumber)
        {
            return this.Store.FindByPhoneNumberAsync(phoneNumber);
        }
    }
}