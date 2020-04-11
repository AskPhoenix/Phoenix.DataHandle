using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Phoenix.Api.Controllers
{
    [Route("api/[controller]")]
    public class SchoolController : BaseController
    {
        private readonly ILogger<SchoolController> _logger;

        public SchoolController(ILogger<SchoolController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            this._logger.LogInformation("Api -> School -> Get");

            return new string[] { "School1", "School2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            this._logger.LogInformation("Api -> School -> Get{id}");

            return "School3";
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
            this._logger.LogInformation("Api -> School -> Delete" + id);
        }
    }
}
