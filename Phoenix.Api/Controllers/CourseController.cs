using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phoenix.Api.Models.Api;
using Phoenix.DataHandle.Main;
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
        public async Task<IEnumerable<CourseApi>> Get()
        {
            this._logger.LogInformation("Api -> Course -> Get");

            IQueryable<Course> courses = this._courseRepository.find();

            return await courses.Select(course => new CourseApi
            {
                id = course.Id,
                Name = course.Name,
                Group = course.Group,
                Level = course.Level,
                Info = course.Info,
                School = new School
                {
                    Id = course.School.Id,
                    Slug = course.School.Slug,
                    Name = course.School.Name,
                }
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<CourseApi> Get(int id)
        {
            this._logger.LogInformation($"Api -> Course -> Get{id}");

            Course course  = await this._courseRepository.find(id);

            return new CourseApi
            {
                id = course.Id,
                Name = course.Name,
                Group = course.Group,
                Level = course.Level,
                Info = course.Info,
                School = new School
                {
                    Id = course.School.Id,
                    Slug = course.School.Slug,
                    Name = course.School.Name,
                }
            };
        }

    }
}
