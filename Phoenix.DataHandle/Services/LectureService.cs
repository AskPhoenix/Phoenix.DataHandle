using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Services
{
    public class LectureService
    {
        private readonly ILogger _logger;
        private readonly Repository<Course> _courseRepository;
        private readonly LectureRepository _lectureRepository;

        public LectureService(PhoenixContext phoenixContext, ILogger<LectureService> logger)
        {
            this._courseRepository = new Repository<Course>(phoenixContext);
            this._courseRepository.include(a => a.Include(b => b.Lecture).Include(b => b.Schedule).ThenInclude(b => b.Classroom).Include(b => b.Schedule).ThenInclude(b => b.Course));

            this._lectureRepository = new LectureRepository(phoenixContext);
            this._lectureRepository.include(a => a.Course);

            this._logger = logger;
        }

        public async Task generateLectures(ICollection<Course> courses, CancellationToken cancellationToken)
        {
            foreach (Course course in courses)
            {
                await this.generateLectures(course, cancellationToken);
            }
        }

        public Task generateLectures(Course course, CancellationToken cancellationToken)
        {
            var period = Enumerable.Range(0, 1 + course.LastDate.Date.Subtract(course.FirstDate.Date).Days)
                 .Select(offset => course.FirstDate.Date.AddDays(offset))
                 .Where(a => a.Date > DateTime.Now.Date)
                 .ToArray();

            foreach (DateTime day in period)
            {
                foreach (var scheduleOfTheDay in course.Schedule.Where(a => a.DayOfWeek == day.DayOfWeek))
                {
                    Lecture lecture = scheduleOfTheDay.Lecture?
                        .Where(a => Utils.getWeekOfYearISO8601(a.StartDateTime) == Utils.getWeekOfYearISO8601(day))
                        .Where(a => a.Status == LectureStatus.Scheduled)
                        .SingleOrDefault(a => a.CreatedBy == LectureCreatedBy.Automatic);

                    if (lecture == null)
                    {
                        lecture = new Lecture
                        {
                            CourseId = scheduleOfTheDay.CourseId,
                            ClassroomId = scheduleOfTheDay.ClassroomId,
                            ScheduleId = scheduleOfTheDay.Id,
                            StartDateTime = day.Add(scheduleOfTheDay.StartTime.TimeOfDay),
                            EndDateTime = day.Add(scheduleOfTheDay.EndTime.TimeOfDay),
                            CreatedBy = LectureCreatedBy.Automatic,
                            Status = LectureStatus.Scheduled,
                        };

                        this._lectureRepository.create(lecture);
                        this._logger.LogInformation($"Lecture created successfully | {course.Name} | {day:dd/MM/yyyy} | {scheduleOfTheDay.StartTime:HH:mm}");
                    }
                    else
                    {
                        lecture.ClassroomId = scheduleOfTheDay.ClassroomId;
                        lecture.CourseId = scheduleOfTheDay.CourseId;
                        lecture.StartDateTime = day.Add(scheduleOfTheDay.StartTime.TimeOfDay);
                        lecture.EndDateTime = day.Add(scheduleOfTheDay.EndTime.TimeOfDay);

                        this._lectureRepository.update(lecture);
                        this._logger.LogInformation($"Lecture updated successfully | {course.Name} | {day:dd/MM/yyyy} | {scheduleOfTheDay.StartTime:HH:mm}");
                    }
                }
            }
            return Task.CompletedTask;
        }

    }
}
