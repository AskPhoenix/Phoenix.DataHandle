﻿using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.Utilities;
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
        private readonly LectureRepository _lectureRepository;

        public LectureService(PhoenixContext phoenixContext, ILogger<LectureService> logger)
        {
            this._lectureRepository = new LectureRepository(phoenixContext);
            this._lectureRepository.Include(a => a.Course);

            this._logger = logger;
        }

        public async Task GenerateLectures(ICollection<Course> courses, CancellationToken cancellationToken, 
            Tense tenseToGenerateFor = Tense.Future, bool quiet = false)
        {
            if (courses is null)
                throw new ArgumentNullException(nameof(courses));

            foreach (Course course in courses)
                await this.GenerateLectures(course, cancellationToken, tenseToGenerateFor, quiet);
        }

        // TODO: Delete or cancel lectures that are no longer in the schedule (e.g. when the schedule changes)
        public async Task GenerateLectures(Course course, CancellationToken cancellationToken, 
            Tense tenseToGenerateFor = Tense.Future, bool quiet = false)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            this._logger.LogInformation($"Start generating lectures for course | {course.School.Name} | {course.NameWithSubcourse}, {course.Level} | {course.FirstDate:dd/MM/yyyy}-{course.LastDate:dd/MM/yyyy} | {course.Schedule.Count} Schedules | {course.Lecture.Count} Lectures");

            var period = Enumerable.Range(0, 1 + course.LastDate.Date.Subtract(course.FirstDate.Date).Days)
                 .Select(offset => course.FirstDate.Date.AddDays(offset));

            if (tenseToGenerateFor == Tense.Past)
                period = period.Where(d => d.Date < DateTime.UtcNow.Date);
            else if (tenseToGenerateFor is Tense.Future)
                period = period.Where(d => d.Date >= DateTime.UtcNow.Date);

            foreach (DateTime day in period)
            {
                foreach (var scheduleOfTheDay in course.Schedule.Where(s => s.DayOfWeek == day.DayOfWeek))
                {
                    Lecture lecture = scheduleOfTheDay.Lecture?
                        .Where(l => CalendarExtensions.GetWeekOfYearISO8601(l.StartDateTime) == CalendarExtensions.GetWeekOfYearISO8601(day))
                        .Where(l => l.StartDateTime.Year == day.Year)
                        .Where(l => l.Status == LectureStatus.Scheduled)
                        .SingleOrDefault(a => a.CreatedBy == LectureCreatedBy.Automatic);

                    if (lecture is null)
                    {
                        lecture = new Lecture
                        {
                            CourseId = scheduleOfTheDay.CourseId,
                            ClassroomId = scheduleOfTheDay.ClassroomId,
                            ScheduleId = scheduleOfTheDay.Id,
                            StartDateTime = new DateTimeOffset(day.Add(scheduleOfTheDay.StartTime.TimeOfDay), scheduleOfTheDay.StartTime.Offset),
                            EndDateTime = new DateTimeOffset(day.Add(scheduleOfTheDay.EndTime.TimeOfDay), scheduleOfTheDay.EndTime.Offset),
                            CreatedBy = LectureCreatedBy.Automatic,
                            Status = LectureStatus.Scheduled,
                        };

                        await this._lectureRepository.Create(lecture, cancellationToken);
                        if (!quiet)
                            this._logger.LogInformation($"Lecture {lecture.Id} created successfully  |  {course.NameWithSubcourse} | {day:dd/MM/yyyy} | {scheduleOfTheDay.DayOfWeek, 10}| {scheduleOfTheDay.StartTime:HH:mm} - {scheduleOfTheDay.EndTime:HH:mm}");
                    }
                    else
                    {
                        lecture.ClassroomId = scheduleOfTheDay.ClassroomId;
                        lecture.CourseId = scheduleOfTheDay.CourseId;
                        lecture.StartDateTime = new DateTimeOffset(day.Add(scheduleOfTheDay.StartTime.TimeOfDay), scheduleOfTheDay.StartTime.Offset);
                        lecture.EndDateTime = new DateTimeOffset(day.Add(scheduleOfTheDay.EndTime.TimeOfDay), scheduleOfTheDay.EndTime.Offset);

                        await this._lectureRepository.Update(lecture, cancellationToken);
                        if (!quiet)
                            this._logger.LogInformation($"Lecture {lecture.Id} updated successfully | {course.NameWithSubcourse} | {day:dd/MM/yyyy} | {scheduleOfTheDay.DayOfWeek, 10}| {scheduleOfTheDay.StartTime:HH:mm} - {scheduleOfTheDay.EndTime:HH:mm}");
                    }
                }
            }
        }
    }
}
