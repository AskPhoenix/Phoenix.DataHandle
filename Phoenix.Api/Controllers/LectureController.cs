using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phoenix.Api.Models.Api;
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

            IQueryable<Lecture> lectures = this._lectureRepository.find();

            return await lectures.Select(lecture => new LectureApi
            {
                id = lecture.Id,
                Status = lecture.Status,
                StartDateTime = lecture.StartDateTime,
                EndDateTime = lecture.EndDateTime,
                Info = lecture.Info,
                Course = new CourseApi
                {
                    id = lecture.Course.Id,
                    Name = lecture.Course.Name,
                    Level = lecture.Course.Level,
                    Group = lecture.Course.Group,
                    Info = lecture.Course.Info
                },
                Classroom = new ClassroomApi
                {
                    id = lecture.Classroom.Id,
                    Name = lecture.Course.Name,
                    Info = lecture.Classroom.Info
                },
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ILecture> Get(int id)
        {
            this._logger.LogInformation($"Api -> Lecture -> Get{id}");

            Lecture lecture = await this._lectureRepository.find(id);

            return new LectureApi
            {
                id = lecture.Id,
                Status = lecture.Status,
                StartDateTime = lecture.StartDateTime,
                EndDateTime = lecture.EndDateTime,
                Info = lecture.Info,
                Course = new CourseApi
                {
                    id = lecture.Course.Id,
                    Name = lecture.Course.Name,
                    Level = lecture.Course.Level,
                    Group = lecture.Course.Group,
                    Info = lecture.Course.Info
                },
                Classroom = new ClassroomApi
                {
                    id = lecture.Classroom.Id,
                    Name = lecture.Course.Name,
                    Info = lecture.Classroom.Info
                },
            };
        }



    }
}
