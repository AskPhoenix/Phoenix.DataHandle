using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Talagozis.AspNetCore.Services.TokenAuthentication;
using Talagozis.AspNetCore.Services.TokenAuthentication.Models;

namespace Phoenix.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly ITokenAuthenticationService _tokenAuthenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ITokenAuthenticationService tokenAuthenticationService, ILogger<AuthenticationController> logger)
        {
            this._tokenAuthenticationService = tokenAuthenticationService;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] TokenRequest tokenRequest)
        {
            this._logger.LogInformation("Api -> Authentication -> Authenticate");

            if (tokenRequest == null)
                throw new ArgumentNullException(nameof(tokenRequest));

            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                string token = await this._tokenAuthenticationService.authenticateAsync(tokenRequest);

                if (string.IsNullOrWhiteSpace(token))
                    return this.Unauthorized(new
                    {
                        code = 1,
                        message = "Bad user name or password"
                    });

                return this.Ok(new TokenResponse
                {
                    token = token
                });
            }
            catch (Exception ex)
            {
                this._logger.LogCritical(1, ex, nameof(this.Authenticate));
                throw;
            }
        }

        

    }
}