    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class LectureRepository : RepositoryAsync<Lecture>
    {
        public LectureRepository(PhoenixContext dbContext) : base(dbContext) { }

        public Task<Lecture> FindSingle(int courseId, DateTime day, TimeSpan time, CancellationToken cancellationToken)
        {
            return this
                .Find()
                .Where(a => a.CourseId == courseId)
                .Where(a => a.StartDateTime.Date == day)
                .SingleOrDefaultAsync(a => a.StartDateTime.TimeOfDay == time, cancellationToken: cancellationToken);
        }

        public IQueryable<Lecture> FindMany(int courseId, DateTime day, bool scheduledOnly = false)
        {
            var lectures = this.Find().
                Where(l => l.CourseId == courseId).
                Where(l => l.StartDateTime.Date == day.Date);

            if (scheduledOnly)
                lectures = lectures.Where(l => l.Status == LectureStatus.Scheduled);

            return lectures;
        }

        public IQueryable<Lecture> FindMany(int[] courseIds, DateTime day, bool scheduledOnly = false)
        {
            if (courseIds == null)
                throw new ArgumentNullException(nameof(courseIds));

            var lectures = new List<Lecture>();

            foreach (int courseId in courseIds)
                lectures.AddRange(this.FindMany(courseId, day, scheduledOnly));

            return lectures.AsQueryable();
        }

        public IQueryable<DateTime> FindClosestLectureDates(int courseId, Tense tense, int dayRange = 5, bool scheduledOnly = false)
        {
            var lectures = this.Find().
                Where(l => l.CourseId == courseId);

            if (tense == Tense.Past)
                lectures.Where(l => l.StartDateTime.ToUniversalTime() < DateTimeOffset.UtcNow);
            else if (tense == Tense.Future)
                lectures.Where(l => l.StartDateTime.ToUniversalTime() >= DateTimeOffset.UtcNow);
            if (scheduledOnly)
                lectures = lectures.Where(l => l.Status == LectureStatus.Scheduled);

            return lectures.
                GroupBy(l => l.StartDateTime.Date).
                Select(g => g.Key).
                OrderByDescending(d => (d.ToUniversalTime() - DateTimeOffset.UtcNow).Duration()).
                Take(dayRange).
                OrderBy(d => d);
        }

        public IQueryable<DateTime> FindClosestLectureDates(int[] courseIds, Tense tense, int dayRange = 5, bool scheduledOnly = false)
        {
            if (courseIds == null)
                throw new ArgumentNullException(nameof(courseIds));

            var lectures = new List<DateTime>(courseIds.Length * dayRange);

            foreach (int courseId in courseIds)
                lectures.AddRange(this.FindClosestLectureDates(courseId, tense, dayRange, scheduledOnly));

            return lectures.AsQueryable();
        }
    }
}
