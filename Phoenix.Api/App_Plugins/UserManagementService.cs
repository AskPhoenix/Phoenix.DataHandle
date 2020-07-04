using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.DataHandle.Identity;
using Talagozis.AspNetCore.Services.TokenAuthentication;
using Talagozis.AspNetCore.Services.TokenAuthentication.Models;

namespace Phoenix.Api.App_Plugins
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ApplicationUserManager _userManager;

        public UserManagementService(ApplicationUserManager userManager)
        {
            this._userManager = userManager;
        }

        public async Task<IAuthenticatedUser> authenticateUserAsync(string username, string password, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser = await this._userManager.FindByNameAsync(username);

            if (applicationUser == null)
                return null;

            if (!applicationUser.PhoneNumberConfirmed)
                return null;

            if (!await this._userManager.CheckPasswordAsync(applicationUser, password))
                return null;

            return new AuthenticatedUser
            {
                username = applicationUser.UserName,
                email = applicationUser.Email,
                roles = (await this._userManager.GetRolesAsync(applicationUser)).ToArray(),
            };
        }
    }
}
