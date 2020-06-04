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
    public class ExerciseController : BaseController
    {
        private readonly ILogger<ExerciseController> _logger;
        private readonly Repository<Exercise> _exerciseRepository;

        public ExerciseController(PhoenixContext phoenixContext, ILogger<ExerciseController> logger)
        {
            this._logger = logger;
            this._exerciseRepository = new Repository<Exercise>(phoenixContext);
        }

        [HttpGet("{id}")]
        public async Task<IExercise> Get(int id)
        {
            this._logger.LogInformation($"Api -> Exercise -> Get{id}");

            Exercise exercise = await this._exerciseRepository.find(id);

            return new ExerciseApi
            {
                id = exercise.Id,
                Name = exercise.Name,
                Page = exercise.Page,
                Book = new BookApi
                {
                    id = exercise.Book.Id,
                    Name = exercise.Book.Name,
                },
                Lecture = new LectureApi
                {
                    id = exercise.Lecture.Id,
                    StartDateTime = exercise.Lecture.StartDateTime,
                    EndDateTime = exercise.Lecture.EndDateTime,
                    Status = exercise.Lecture.Status,
                    Info = exercise.Lecture.Info,
                    Course = new CourseApi
                    {
                        id = exercise.Lecture.Course.Id
                    },
                    Classroom = new ClassroomApi
                    {
                        id = exercise.Lecture.Classroom.Id
                    }
                }
            };
        }

        [HttpPost]
        public async Task<ExerciseApi> Post([FromBody] ExerciseApi exerciseApi)
        {
            this._logger.LogInformation("Api -> Exercise -> Post");

            Exercise exercise = new Exercise
            {
                Name = exerciseApi.Name,
                Page = exerciseApi.Page,
                Info = exerciseApi.Info,
                LectureId = exerciseApi.Lecture.id,
                BookId = exerciseApi.Book.id,
            };

            exercise = this._exerciseRepository.create(exercise);

            exercise = await this._exerciseRepository.find(exercise.Id);

            return new ExerciseApi
            {
                id = exercise.Id,
                Name = exercise.Name,
                Page = exercise.Page,
                Book = exercise.Book != null
                    ? new BookApi
                    {
                        id = exercise.Book.Id,
                        Name = exercise.Book.Name,
                    }
                    : null,
                Lecture = exercise.Lecture != null
                    ? new LectureApi
                    {
                        id = exercise.Lecture.Id,
                        StartDateTime = exercise.Lecture.StartDateTime,
                        EndDateTime = exercise.Lecture.EndDateTime,
                        Status = exercise.Lecture.Status,
                        Info = exercise.Lecture.Info,
                        Course = exercise.Lecture.Course != null
                            ? new CourseApi
                            {
                                id = exercise.Lecture.Course.Id
                            }
                            : null,
                        Classroom = exercise.Lecture.Classroom != null
                            ? new ClassroomApi
                            {
                                id = exercise.Lecture.Classroom.Id
                            }
                            : null
                    }
                    : null
            };
        }

        [HttpPut("{id}")]
        public async Task<ExerciseApi> Put(int id, [FromBody] ExerciseApi exerciseApi)
        {
            this._logger.LogInformation("Api -> Exercise -> Put");

            Exercise exercise = new Exercise
            {
                Id = exerciseApi.id,
                Name = exerciseApi.Name,
                Page = exerciseApi.Page,
                Info = exerciseApi.Info,
                LectureId = exerciseApi.Lecture.id,
                BookId = exerciseApi.Book.id,
            };

            exercise = this._exerciseRepository.update(exercise);

            exercise = await this._exerciseRepository.find(exercise.Id);

            return new ExerciseApi
            {
                id = exercise.Id,
                Name = exercise.Name,
                Page = exercise.Page,
                Book = exercise.Book != null
                    ? new BookApi
                    {
                        id = exercise.Book.Id,
                        Name = exercise.Book.Name,
                    }
                    : null,
                Lecture = exercise.Lecture != null
                    ? new LectureApi
                    {
                        id = exercise.Lecture.Id,
                        StartDateTime = exercise.Lecture.StartDateTime,
                        EndDateTime = exercise.Lecture.EndDateTime,
                        Status = exercise.Lecture.Status,
                        Info = exercise.Lecture.Info,
                        Course = exercise.Lecture.Course != null
                            ? new CourseApi
                            {
                                id = exercise.Lecture.Course.Id
                            }
                            : null,
                        Classroom = exercise.Lecture.Classroom != null
                            ? new ClassroomApi
                            {
                                id = exercise.Lecture.Classroom.Id
                            }
                            : null
                    }
                    : null
            };
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this._logger.LogInformation($"Api -> Exercise -> Get -> {id}");

            this._exerciseRepository.delete(id);
        }

    }
}
