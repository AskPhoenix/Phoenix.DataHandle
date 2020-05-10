using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly Repository<User> _userRepository;

        public UserController(PhoenixContext phoenixContext, ILogger<UserController> logger)
        {
            this._logger = logger;
            this._userRepository = new Repository<User>(phoenixContext);
        }

        [HttpGet]
        public async Task<IEnumerable<IUser>> Get()
        {
            this._logger.LogInformation("Api -> User -> Get");

            return await this._userRepository.find().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IUser> Get(int id)
        {
            this._logger.LogInformation($"Api -> User -> Get{id}");

            return await this._userRepository.find(id);
        }



    }
}
