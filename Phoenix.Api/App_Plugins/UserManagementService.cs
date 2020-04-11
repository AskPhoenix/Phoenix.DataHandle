using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talagozis.AspNetCore.Services.TokenAuthentication;
using Talagozis.AspNetCore.Services.TokenAuthentication.Models;

namespace Phoenix.Api.App_Plugins
{
    public class UserManagementService : IUserManagementService
    {
        //private readonly ApplicationUserManager _userManager;

        //public UserManagementService(ApplicationUserManager userManager)
        //{
        //    this._userManager = userManager;
        //}

        public Task<AuthenticatedUser> isValidUserAsync(string username, string password)
        {
            //ApplicationUser applicationUser = await this._userManager.FindByNameAsync(username);

            //if (applicationUser == null)
            //    return null;

            //if (!await this._userManager.CheckPasswordAsync(applicationUser, password))
            //    return null;

            var authenticatedUser = new AuthenticatedUser
            {
                username = "test@askphoenix.gr",
                email = "test@askphoenix.gr",
                roles = new string[] {"admin"}
                //username = applicationUser.UserName,
                //email = applicationUser.Email,
                //roles = (await this._userManager.GetRolesAsync(applicationUser)).ToArray(),
            };

            return Task.FromResult(authenticatedUser);
        }
    }
}
