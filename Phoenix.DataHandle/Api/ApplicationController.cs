using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using System.Security.Claims;

namespace Phoenix.DataHandle.Api
{
    public abstract class ApplicationController : Controller
    {
        protected ApplicationUser? AppUser { get; private set; }
        protected User? PhoenixUser { get; private set; }
        
        protected readonly UserRepository _userRepository;
        protected readonly ApplicationUserManager _userManager;
        protected readonly ILogger<ApplicationController> _logger;

        protected ApplicationController(
            PhoenixContext phoenixContext,
            ApplicationUserManager userManager,
            ILogger<ApplicationController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _userRepository = new(phoenixContext, nonObviatedOnly: false);
        }

        protected bool CheckUserAuth()
        {
            bool isAuth = this.AppUser is not null;

            if (!isAuth)
                _logger.LogError("User is not authorized");

            return isAuth;
        }

        protected IEnumerable<School>? FindSchools(bool nonObviatedOnly = true)
        {
            return this.PhoenixUser?.Schools
                .Where(s => !nonObviatedOnly || (!s.ObviatedAt.HasValue && nonObviatedOnly));
        }

        protected School? FindSchool(int schoolId, bool nonObviatedOnly = true)
        {
            return this.FindSchools(nonObviatedOnly)?
                .SingleOrDefault(s => s.Id == schoolId);
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is null)
            {
                _logger.LogInformation("No Identity is provided");

                await base.OnActionExecutionAsync(context, next);
                return;
            }

            var userClaims = identity.Claims;
            if (!userClaims.Any(c => c.Type == ClaimTypes.NameIdentifier))
            {
                _logger.LogInformation("No claim for username found in the Identity");

                await base.OnActionExecutionAsync(context, next);
                return;
            }

            this.AppUser = await _userManager
                .FindByNameAsync(userClaims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (this.AppUser is null)
            {
                _logger.LogInformation("No user found with the specified username");

                await base.OnActionExecutionAsync(context, next);
                return;
            }

            this.PhoenixUser = await _userRepository.FindPrimaryAsync(this.AppUser.Id);
            if (this.PhoenixUser?.ObviatedAt.HasValue ?? false)
            {
                this.PhoenixUser = null;
                this.AppUser = null;

                await base.OnActionExecutionAsync(context, next);
                return;
            }
            
            _logger.LogInformation("User with ID {Id} is authorized", this.AppUser.Id);

            await base.OnActionExecutionAsync(context, next);
            return;
        }
    }
}
