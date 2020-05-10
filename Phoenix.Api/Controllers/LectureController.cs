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
    public class LectureController : BaseController
    {
        private readonly ILogger<LectureController> _logger;
        private readonly Repository<Lecture> _lectureRepository;

        public LectureController(PhoenixContext phoenixContext, ILogger<LectureController> logger)
        {
            this._logger = logger;
            this._lectureRepository = new Repository<Lecture>(phoenixContext);
        }

        [HttpGet]
        public async Task<IEnumerable<ILecture>> Get()
        {
            this._logger.LogInformation("Api -> Lecture -> Get");

            return await this._lectureRepository.find().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ILecture> Get(int id)
        {
            this._logger.LogInformation($"Api -> Lecture -> Get{id}");

            return await this._lectureRepository.find(id);
        }



    }
}
