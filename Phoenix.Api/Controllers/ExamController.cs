using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Phoenix.Api.Models.Api;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;

namespace Phoenix.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExamController : BaseController
    {
        private readonly ILogger<ExamController> _logger;
        private readonly Repository<Exam> _examRepository;

        public ExamController(PhoenixContext phoenixContext, ILogger<ExamController> logger)
        {
            this._logger = logger;
            this._examRepository = new Repository<Exam>(phoenixContext);
            this._examRepository.include(a => a.Lecture);
        }

        [HttpGet("{id}")]
        public async Task<IExam> Get(int id)
        {
            this._logger.LogInformation($"Api -> Exam -> Get{id}");

            Exam exam = await this._examRepository.find(id);

            return new ExamApi
            {
                id = exam.Id,
                Name = exam.Name,
                Comments = exam.Comments,
                Materials = exam.Material != null
                    ? exam.Material.Select(material => new MaterialApi
                    {
                        id = material.Id,
                        Chapter = material.Chapter,
                        Section = material.Section,
                        Comments = material.Comments,
                        Book = material.Book != null
                            ? new BookApi
                            {
                                id = material.Book.Id,
                                Name = material.Book.Name,
                            }
                            : null
                    }).ToList()
                    : new List<MaterialApi>(),
                Lecture = exam.Lecture != null
                    ? new LectureApi
                    {
                        id = exam.Lecture.Id,
                        StartDateTime = exam.Lecture.StartDateTime,
                        EndDateTime = exam.Lecture.EndDateTime,
                        Status = exam.Lecture.Status,
                        Info = exam.Lecture.Info,
                        Course = exam.Lecture.Course != null
                            ? new CourseApi
                            {
                                id = exam.Lecture.Course.Id
                            }
                            : null,
                        Classroom = exam.Lecture.Classroom != null
                            ? new ClassroomApi
                            {
                                id = exam.Lecture.Classroom.Id
                            }
                            : null
                    }
                    : null,
            };
        }

        [HttpPost]
        public async Task<ExamApi> Post([FromBody] ExamApi examApi)
        {
            this._logger.LogInformation("Api -> Exam -> Post");

            Exam exam = new Exam
            {
                Name = examApi.Name,
                Comments = examApi.Comments,
                LectureId = examApi.Lecture.id
            };

            exam = this._examRepository.create(exam);

            exam = await this._examRepository.find(exam.Id);

            return new ExamApi
            {
                id = exam.Id,
                Name = exam.Name,
                Comments = exam.Comments,
                Materials = exam.Material != null
                    ? exam.Material.Select(material => new MaterialApi
                    {
                        id = material.Id,
                        Chapter = material.Chapter,
                        Section = material.Section,
                        Comments = material.Comments,
                        Book = material.Book != null
                            ? new BookApi
                            {
                                id = material.Book.Id,
                                Name = material.Book.Name,
                            }
                            : null
                    }).ToList()
                    : new List<MaterialApi>(),
                Lecture = exam.Lecture != null
                    ? new LectureApi
                    {
                        id = exam.Lecture.Id,
                        StartDateTime = exam.Lecture.StartDateTime,
                        EndDateTime = exam.Lecture.EndDateTime,
                        Status = exam.Lecture.Status,
                        Info = exam.Lecture.Info,
                        Course = exam.Lecture.Course != null
                            ? new CourseApi
                            {
                                id = exam.Lecture.Course.Id
                            }
                            : null,
                        Classroom = exam.Lecture.Classroom != null
                            ? new ClassroomApi
                            {
                                id = exam.Lecture.Classroom.Id
                            }
                            : null
                    }
                    : null,
            };
        }

        [HttpPut("{id}")]
        public async Task<ExamApi> Put(int id, [FromBody] ExamApi examApi)
        {
            this._logger.LogInformation("Api -> Exam -> Put");

            Exam exam = new Exam
            {
                Id = examApi.id,
                Name = examApi.Name,
                Comments = examApi.Comments,
                LectureId = examApi.Lecture.id
            };

            exam = this._examRepository.update(exam);

            exam = await this._examRepository.find(exam.Id);

            return new ExamApi
            {
                id = exam.Id,
                Name = exam.Name,
                Comments = exam.Comments,
                Materials = exam.Material != null
                    ? exam.Material.Select(material => new MaterialApi
                    {
                        id = material.Id,
                        Chapter = material.Chapter,
                        Section = material.Section,
                        Comments = material.Comments,
                        Book = material.Book != null
                            ? new BookApi
                            {
                                id = material.Book.Id,
                                Name = material.Book.Name,
                            }
                            : null
                    }).ToList()
                    : new List<MaterialApi>(),
                Lecture = exam.Lecture != null
                    ? new LectureApi
                    {
                        id = exam.Lecture.Id,
                        StartDateTime = exam.Lecture.StartDateTime,
                        EndDateTime = exam.Lecture.EndDateTime,
                        Status = exam.Lecture.Status,
                        Info = exam.Lecture.Info,
                        Course = exam.Lecture.Course != null
                            ? new CourseApi
                            {
                                id = exam.Lecture.Course.Id
                            }
                            : null,
                        Classroom = exam.Lecture.Classroom != null
                            ? new ClassroomApi
                            {
                                id = exam.Lecture.Classroom.Id
                            }
                            : null
                    }
                    : null,
            };
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this._logger.LogInformation($"Api -> Exam -> Get -> {id}");

            this._examRepository.delete(id);
        }

    }
}
