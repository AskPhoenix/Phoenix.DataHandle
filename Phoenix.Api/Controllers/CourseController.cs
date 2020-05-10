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
    public class CourseController : BaseController
    {
        private readonly ILogger<CourseController> _logger;
        private readonly Repository<Course> _courseRepository;

        public CourseController(PhoenixContext phoenixContext, ILogger<CourseController> logger)
        {
            this._logger = logger;
            this._courseRepository = new Repository<Course>(phoenixContext);
        }

        [HttpGet]
        public async Task<IEnumerable<ICourse>> Get()
        {
            this._logger.LogInformation("Api -> Course -> Get");

            return await this._courseRepository.find().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ICourse> Get(int id)
        {
            this._logger.LogInformation($"Api -> Course -> Get{id}");

            return await this._courseRepository.find(id);
        }

    }
}
