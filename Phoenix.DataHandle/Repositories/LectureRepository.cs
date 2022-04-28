using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class LectureRepository : ObviableRepository<Lecture>
    {
        public bool SearchNonCancelledOnly { get; set; } = true;

        public LectureRepository(PhoenixContext phoenixContext)
            : base(phoenixContext) 
        { 
        }

        public static Expression<Func<Lecture, bool>> GetUniqueExpression(
            int courseId, DateTimeOffset startDateTime)
        {
            return l => l.CourseId == courseId && l.StartDateTime == startDateTime;
        }

        #region Find Unique

        public Task<Lecture?> FindUniqueAsync(int courseId, DateTimeOffset startDateTime, 
            CancellationToken cancellationToken = default)
        {
            return FindUniqueAsync(GetUniqueExpression(courseId, startDateTime), 
                cancellationToken);
        }

        public Task<Lecture?> FindUniqueAsync(int courseId, ILecture lecture,
            CancellationToken cancellationToken = default)
        {
            if (lecture is null)
                throw new ArgumentNullException(nameof(lecture));

            return FindUniqueAsync(courseId, lecture.StartDateTime,
                cancellationToken);
        }

        #endregion

        #region Search

        public IQueryable<Lecture> Search(int? courseId = null, int? classroomId = null, int? scheduleId = null)
        {
            var lectures = Find();

            if (courseId.HasValue)
                lectures = lectures.Where(l => l.CourseId == courseId);
            if (classroomId.HasValue)
                lectures = lectures.Where(l => l.ClassroomId == classroomId);
            if (scheduleId.HasValue)
                lectures = lectures.Where(l => l.ScheduleId == scheduleId);

            if (SearchNonCancelledOnly)
                lectures = lectures.Where(l => !l.IsCancelled);

            return lectures;
        }

        public IQueryable<Lecture> Search(int courseId, DateTime day)
        {
            var lectures = Find().
                Where(l => l.CourseId == courseId && l.StartDateTime.Date == day);

            if (SearchNonCancelledOnly)
                lectures = lectures.Where(l => !l.IsCancelled);

            return lectures;
        }

        // This is not a wrapper of Search for each course
        public IQueryable<Lecture> Search(int[] courseIds, DateTime day)
        {
            if (courseIds == null)
                throw new ArgumentNullException(nameof(courseIds));

            var lectures = Find().
                Where(l => courseIds.Contains(l.CourseId) && l.StartDateTime.Date == day);

            if (SearchNonCancelledOnly)
                lectures = lectures.Where(l => !l.IsCancelled);

            return lectures;
        }

        public IQueryable<Lecture> Search(int courseId, Tense tense,
            DateTimeOffset reference, int max = 5)
        {
            var lectures = Find().
                Where(l => l.CourseId == courseId);

            return SearchClosest(lectures, tense, reference, max);
        }

        // This is not a wrapper of Search for each course
        public IQueryable<Lecture> Search(int[] courseIds, Tense tense,
            DateTimeOffset reference, int max = 5)
        {
            if (courseIds is null)
                throw new ArgumentNullException(nameof(courseIds));

            var lectures = Find().
                Where(l => courseIds.Contains(l.CourseId));

            return SearchClosest(lectures, tense, reference, max);
        }

        // TODO: Check if this can be translated to SQL query
        private IQueryable<Lecture> SearchClosest(IQueryable<Lecture> lectures, Tense tense,
            DateTimeOffset reference, int max = 5)
        {
            if (tense == Tense.Past)
                lectures = lectures.Where(l => l.StartDateTime < reference);
            else if (tense == Tense.Future)
                lectures = lectures.Where(l => l.StartDateTime >= reference);

            if (SearchNonCancelledOnly)
                lectures = lectures.Where(l => !l.IsCancelled);

            return lectures.OrderBy(l => (l.StartDateTime - reference).Duration())
                .Take(max);
        }

        #endregion
    }
}
