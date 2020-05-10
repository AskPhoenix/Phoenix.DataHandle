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
    public class ScheduleController : BaseController
    {
        private readonly ILogger<ScheduleController> _logger;
        private readonly Repository<Schedule> _scheduleRepository;

        public ScheduleController(PhoenixContext phoenixContext, ILogger<ScheduleController> logger)
        {
            this._logger = logger;
            this._scheduleRepository = new Repository<Schedule>(phoenixContext);
        }

        [HttpGet]
        public async Task<IEnumerable<ISchedule>> Get()
        {
            this._logger.LogInformation("Api -> Schedule -> Get");

            return await this._scheduleRepository.find().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ISchedule> Get(int id)
        {
            this._logger.LogInformation($"Api -> Schedule -> Get{id}");

            return await this._scheduleRepository.find(id);
        }

    }
}
