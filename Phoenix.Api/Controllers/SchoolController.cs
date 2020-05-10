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
    public class SchoolController : BaseController
    {
        private readonly ILogger<SchoolController> _logger;
        private readonly Repository<School> _schoolRepository;

        public SchoolController(PhoenixContext phoenixContext, ILogger<SchoolController> logger)
        {
            this._logger = logger;
            this._schoolRepository = new Repository<School>(phoenixContext);
        }

        [HttpGet]
        public async Task<IEnumerable<ISchool>> Get()
        {
            this._logger.LogInformation("Api -> School -> Get");

            return await this._schoolRepository.find().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ISchool> Get(int id)
        {
            this._logger.LogInformation($"Api -> School -> Get{id}");

            return await this._schoolRepository.find(id);
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
