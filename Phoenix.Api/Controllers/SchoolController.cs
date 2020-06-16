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
        private readonly Repository<Classroom> _classroomRepository;
        private readonly Repository<Course> _courseRepository;

        public SchoolController(PhoenixContext phoenixContext, ILogger<SchoolController> logger)
        {
            this._logger = logger;
            this._schoolRepository = new Repository<School>(phoenixContext);
            this._classroomRepository = new Repository<Classroom>(phoenixContext);
            this._courseRepository = new Repository<Course>(phoenixContext);
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

        [HttpGet("{id}/Classroom")]
        public async Task<IEnumerable<ClassroomApi>> GetClassrooms(int id)
        {
            this._logger.LogInformation($"Api -> School -> Get{id} -> Classrooms");

            IQueryable<Classroom> classrooms = this._classroomRepository.find().Where(a => a.School.Id == id);

            return await classrooms.Select(classroom => new ClassroomApi
            {
                id = classroom.Id,
                Name = classroom.Name,
                Info = classroom.Info,
            }).ToListAsync();
        }

        [HttpGet("{id}/Course")]
        public async Task<IEnumerable<CourseApi>> GetCourses(int id)
        {
            this._logger.LogInformation($"Api -> School -> Get{id} -> Courses");

            IQueryable<Course> courses = this._courseRepository.find().Where(a => a.School.Id == id);

            return await courses.Select(course => new CourseApi
            {
                id = course.Id,
                Name = course.Name,
                Group = course.Group,
                SubCourse = course.SubCourse,
                Level = course.Level,
                Info = course.Info
            }).ToListAsync();
        }


    }
}
