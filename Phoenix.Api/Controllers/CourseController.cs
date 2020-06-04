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
        private readonly Repository<Lecture> _lectureRepository;
        private readonly Repository<Schedule> _scheduleRepository;

        public CourseController(PhoenixContext phoenixContext, ILogger<CourseController> logger)
        {
            this._logger = logger;
            this._courseRepository = new Repository<Course>(phoenixContext);
            this._lectureRepository = new Repository<Lecture>(phoenixContext);
            this._scheduleRepository = new Repository<Schedule>(phoenixContext);
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

        [HttpGet("{id}/Lecture")]
        public async Task<IEnumerable<LectureApi>> GetLectures(int id)
        {
            this._logger.LogInformation($"Api -> Course -> Get{id} -> Lecture");

            IQueryable<Lecture> lectures = this._lectureRepository.find().Where(a => a.CourseId == id);

            return await lectures.Select(lecture => new LectureApi
            {
                id = lecture.Id,
                Status = lecture.Status,
                StartDateTime = lecture.StartDateTime,
                EndDateTime = lecture.EndDateTime,
                Info = lecture.Info,
                Classroom = new ClassroomApi
                {
                    id = lecture.Classroom.Id,
                    Name = lecture.Classroom.Name,
                    Info = lecture.Classroom.Info
                },
                Exercises = lecture.Exercise.Select(a => new ExerciseApi
                {
                    id = a.Id,
                    Name = a.Name,
                }).ToList(),
            }).ToListAsync();
        }

        [HttpGet("{id}/Schedule")]
        public async Task<IEnumerable<ScheduleApi>> GetSchedules(int id)
        {
            this._logger.LogInformation($"Api -> Course -> Get{id} -> Schedule");

            IQueryable<Schedule> schedules = this._scheduleRepository.find().Where(a => a.CourseId == id);

            return await schedules.Select(schedule => new ScheduleApi
            {
                id = schedule.Id,
                DayOfWeek = schedule.DayOfWeek,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                Info = schedule.Info,
                Classroom = new ClassroomApi
                {
                    id = schedule.Classroom.Id,
                    Name = schedule.Classroom.Name,
                    Info = schedule.Classroom.Info
                },
            }).ToListAsync();
        }


    }
}
