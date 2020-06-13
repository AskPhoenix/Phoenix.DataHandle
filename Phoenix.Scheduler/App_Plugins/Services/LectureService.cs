using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JustScheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;

namespace Phoenix.Scheduler.App_Plugins.Services
{
    public class LectureService : IJob
    {
        private readonly ILogger _logger;
        private readonly Repository<Course> _courseRepository;
        private readonly Repository<Schedule> _scheduleRepository;
        private readonly LectureRepository _lectureRepository;

        public LectureService(PhoenixContext phoenixContext, ILogger<LectureService> logger)
        {
            this._courseRepository = new Repository<Course>(phoenixContext);
            this._courseRepository.include(a => a.Lecture, a => a.Schedule);

            this._scheduleRepository = new Repository<Schedule>(phoenixContext);
            this._scheduleRepository.include(a => a.Course);
            this._lectureRepository = new LectureRepository(phoenixContext);
            this._lectureRepository.include(a => a.Course);
            this._logger = logger;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            var courses = await this._courseRepository.find().ToListAsync(cancellationToken);
            courses = courses.Where(a => a.LastDate.Date >= DateTime.Now.Date.AddDays(1)).ToList();

            foreach (Course course in courses)
            {
                await this.generateLectures(course, cancellationToken);
            }

            this._logger.LogInformation("LectureService scheduler completed successfully");
        }

        private Task generateLectures(Course course, CancellationToken cancellationToken)
        {
            var period = Enumerable.Range(0, 1 + course.LastDate.Date.Subtract(course.FirstDate.Date).Days)
                 .Select(offset => course.FirstDate.Date.AddDays(offset))
                 .ToArray();

            foreach (DateTime day in period)
            {
                foreach (var scheduleOfTheDay in course.Schedule.Where(a => a.DayOfWeek == day.DayOfWeek))
                {
                    Lecture lecture = scheduleOfTheDay.Lecture?
                        .Where(a => getWeekOfYearISO8601(a.StartDateTime) == getWeekOfYearISO8601(day))
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
                            Info = string.Empty,
                            Status = LectureStatus.Scheduled,
                        };

                        this._lectureRepository.create(lecture);
                        this._logger.LogInformation($"Lecture created successfully | {course.Name}| {day:dd/MM/yyyy} | {scheduleOfTheDay.StartTime:HH:mm}");
                    }
                    else
                    {
                        lecture.ClassroomId = scheduleOfTheDay.ClassroomId;
                        lecture.CourseId = scheduleOfTheDay.CourseId;
                        lecture.StartDateTime = day.Add(scheduleOfTheDay.StartTime.TimeOfDay);
                        lecture.EndDateTime = day.Add(scheduleOfTheDay.EndTime.TimeOfDay);

                        this._lectureRepository.update(lecture);
                        this._logger.LogInformation($"Lecture updated successfully | {course.Name}| {day:dd/MM/yyyy} | {scheduleOfTheDay.StartTime:HH:mm}");
                    }
                }
            }
            return Task.CompletedTask;
        }


        private static int getWeekOfYearISO8601(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
}
