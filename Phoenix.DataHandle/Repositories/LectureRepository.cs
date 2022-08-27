using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using Phoenix.DataHandle.Repositories.Extensions;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class LectureRepository : ObviableRepository<Lecture>,
        ISetNullDeleteRule<Lecture>, ICascadeDeleteRule<Lecture>
    {
        public bool SearchNonCancelledOnly { get; set; } = true;
        public bool SearchWithExamsOnly { get; set; } = false;

        public LectureRepository(PhoenixContext phoenixContext)
            : base(phoenixContext) 
        { 
        }

        public LectureRepository(PhoenixContext phoenixContext, bool nonObviatedOnly)
            : base(phoenixContext, nonObviatedOnly)
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

        public Task<Lecture?> FindUniqueAsync(int courseId, ILectureBase lecture,
            CancellationToken cancellationToken = default)
        {
            if (lecture is null)
                throw new ArgumentNullException(nameof(lecture));

            return FindUniqueAsync(courseId, lecture.StartDateTime,
                cancellationToken);
        }

        #endregion

        #region Search

        private IQueryable<Lecture> Search()
        {
            var lectures = Find();

            if (SearchNonCancelledOnly)
                lectures = lectures.Where(l => !l.IsCancelled);
            if (SearchWithExamsOnly)
                lectures = lectures.Where(l => l.Exams.Any());

            return lectures;
        }

        public IQueryable<Lecture> Search(int? courseId = null, int? classroomId = null, int? scheduleId = null)
        {
            var lectures = Search();

            if (courseId.HasValue)
                lectures = lectures.Where(l => l.CourseId == courseId);
            if (classroomId.HasValue)
                lectures = lectures.Where(l => l.ClassroomId == classroomId);
            if (scheduleId.HasValue)
                lectures = lectures.Where(l => l.ScheduleId == scheduleId);

            return lectures;
        }

        public IQueryable<Lecture> Search(int courseId, DateTime date)
        {
            return Search(new[] { courseId }, date);
        }

        public IQueryable<Lecture> Search(int[] courseIds, DateTime date)
        {
            if (courseIds is null)
                throw new ArgumentNullException(nameof(courseIds));

            return Search().Where(l => courseIds.Contains(l.CourseId) && l.StartDateTime.Date == date.Date);
        }

        public IQueryable<Lecture> Search(int courseId, Tense tense,
            DateTimeOffset reference, int max = 5)
        {
            return Search(new[] { courseId }, tense, reference, max);
        }

        public IQueryable<Lecture> Search(int[] courseIds, Tense tense,
            DateTimeOffset reference, int max = 5)
        {
            if (courseIds is null)
                throw new ArgumentNullException(nameof(courseIds));

            var lectures = Search().
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

            return lectures.OrderBy(l => (l.StartDateTime - reference).Duration()).Take(max);
        }

        #endregion

        #region Delete

        public void SetNullOnDelete(Lecture lecture)
        {
            lecture.Attendees.Clear();
            // TODO: Check if needs to set InverseLecture to null
        }

        public async Task CascadeOnDeleteAsync(Lecture lecture,
            CancellationToken cancellationToken = default)
        {
            await new ExamRepository(DbContext).DeleteRangeAsync(lecture.Exams, cancellationToken);
            await new ExerciseRepository(DbContext).DeleteRangeAsync(lecture.Exercises, cancellationToken);
        }

        public async Task CascadeRangeOnDeleteAsync(IEnumerable<Lecture> lectures,
            CancellationToken cancellationToken = default)
        {
            await new ExamRepository(DbContext).DeleteRangeAsync(lectures.SelectMany(l => l.Exams),
                cancellationToken);
            await new ExerciseRepository(DbContext).DeleteRangeAsync(lectures.SelectMany(l => l.Exercises),
                cancellationToken);
        }

        #endregion
    }
}
