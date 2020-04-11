using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Entities;
using Phoenix.DataHandle.Models;

namespace Phoenix.Api.Controllers
{
    [Route("api/[controller]")]
    public class SchoolController : BaseController
    {
        private readonly ILogger<SchoolController> _logger;
        private readonly PhoenixContext _phoenixContext;

        public SchoolController(ILogger<SchoolController> logger, PhoenixContext phoenixContext)
        {
            this._logger = logger;
            this._phoenixContext = phoenixContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ISchool>> Get()
        {
            this._logger.LogInformation("Api -> School -> Get");

            return await this._phoenixContext.School.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ISchool> Get(int id)
        {
            this._logger.LogInformation($"Api -> School -> Get{id}");

            return await this._phoenixContext.School.SingleAsync(a => a.id == id);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
            this._logger.LogInformation("Api -> School -> Post");
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            this._logger.LogInformation("Api -> School -> Put");
        }

        [HttpDelete("{id}")]
        public void Delete(int id) 
        { 
            this._logger.LogInformation($"Api -> School -> Get{id}");
        }
    }
}
