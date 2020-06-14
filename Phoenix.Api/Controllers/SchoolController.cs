using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phoenix.Api.Models.Api;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;

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
        public async Task<IEnumerable<SchoolApi>> Get()
        {
            this._logger.LogInformation("Api -> School -> Get");

            IQueryable<School> schools = this._schoolRepository.find();

            return await schools.Select(school => new SchoolApi
            {
                id = school.Id,
                Name = school.Name,
                Slug = school.Slug,
                AddressLine = school.AddressLine,
                City = school.City,
                Endpoint = school.Endpoint,
                FacebookPageId = school.FacebookPageId,
                Info = school.Info,
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<SchoolApi> Get(int id)
        {
            this._logger.LogInformation($"Api -> School -> Get{id}");

            School school = await this._schoolRepository.find(id);

            return new SchoolApi
            {
                id = school.Id,
                Name = school.Name,
                Slug = school.Slug,
                AddressLine = school.AddressLine,
                City = school.City,
                Endpoint = school.Endpoint,
                FacebookPageId = school.FacebookPageId,
                Info = school.Info,
            };
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
