using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.DataEntry.Types.Uniques;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories.Extensions;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class CourseRepository : ObviableRepository<Course>,
        ISetNullDeleteRule<Course>, ICascadeDeleteRule<Course>
    {
        public CourseRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        public CourseRepository(PhoenixContext phoenixContext, bool nonObviatedOnly)
            : base(phoenixContext, nonObviatedOnly)
        {
        }

        public static Expression<Func<Course, bool>> GetUniqueExpression(
            int schoolId, short courseCode)
        {
            return c => c.SchoolId == schoolId && c.Code == courseCode;
        }

        public static Expression<Func<Course, bool>> GetUniqueExpression(
            CourseUnique courseUq)
        {
            if (courseUq is null)
                throw new ArgumentNullException(nameof(courseUq));

            return c => c.School.Code == courseUq.SchoolUnique.Code && c.Code == courseUq.Code;
        }

        #region Find Unique

        public Task<Course?> FindUniqueAsync(int schoolId, short courseCode,
            CancellationToken cancellationToken = default)
        {
            return FindUniqueAsync(GetUniqueExpression(schoolId, courseCode),
                cancellationToken);
        }

        public Task<Course?> FindUniqueAsync(int schoolId, ICourseBase course,
            CancellationToken cancellationToken = default)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            return FindUniqueAsync(schoolId, course.Code,
                cancellationToken);
        }

        public Task<Course?> FindUniqueAsync(CourseUnique courseUnique,
            CancellationToken cancellationToken = default)
        {
            return FindUniqueAsync(GetUniqueExpression(courseUnique),
                cancellationToken);
        }

        #endregion

        #region Delete

        public void SetNullOnDelete(Course course)
        {
            course.Broadcasts.Clear();
            course.Books.Clear();
            course.Users.Clear();
            course.Grades.Clear();
        }

        public async Task CascadeOnDeleteAsync(Course course,
            CancellationToken cancellationToken = default)
        {
            await new LectureRepository(DbContext).DeleteRangeAsync(course.Lectures, cancellationToken);
            await new ScheduleRepository(DbContext).DeleteRangeAsync(course.Schedules, cancellationToken);
        }

        public async Task CascadeRangeOnDeleteAsync(IEnumerable<Course> courses,
            CancellationToken cancellationToken = default)
        {
            await new LectureRepository(DbContext).DeleteRangeAsync(courses.SelectMany(c => c.Lectures),
                cancellationToken);
            await new ScheduleRepository(DbContext).DeleteRangeAsync(courses.SelectMany(c => c.Schedules),
                cancellationToken);
        }

        #endregion
    }
}
