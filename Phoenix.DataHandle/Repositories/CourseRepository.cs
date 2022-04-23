using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class CourseRepository : ObviableRepository<Course>
    {
        public CourseRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        public static Expression<Func<Course, bool>> GetUniqueExpression(
            int schoolId, short courseCode)
        {
            return c => c.SchoolId == schoolId && c.Code == courseCode;
        }

        public static Expression<Func<Course, bool>> GetUniqueExpression(
            SchoolUnique schoolUq, short courseCode)
        {
            return c => c.School.Code == schoolUq.Code && c.Code == courseCode;
        }

        #region Find Unique

        public Course? FindUnique(int schoolId, short courseCode)
        {
            return FindUnique(GetUniqueExpression(schoolId, courseCode));
        }

        public Course? FindUnique(SchoolUnique schoolUq, short courseCode)
        {
            return FindUnique(GetUniqueExpression(schoolUq, courseCode));
        }

        public Course? FindUnique(int schoolId, ICourse course)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            return FindUnique(schoolId, course.Code);
        }

        public Course? FindUnique(SchoolUnique schoolUq, ICourse course)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            return FindUnique(schoolUq, course.Code);
        }

        public Course? FindUnique(CourseUnique courseUnique)
        {
            return FindUnique(courseUnique.SchoolUnique, courseUnique.Code);
        }

        public async Task<Course?> FindUniqueAsync(int schoolId, short courseCode,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(schoolId, courseCode),
                cancellationToken);
        }

        public async Task<Course?> FindUniqueAsync(SchoolUnique schoolUq, short courseCode,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(schoolUq, courseCode),
                cancellationToken);
        }

        public async Task<Course?> FindUniqueAsync(int schoolId, ICourse course,
            CancellationToken cancellationToken = default)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            return await FindUniqueAsync(schoolId, course.Code,
                cancellationToken);
        }

        public async Task<Course?> FindUniqueAsync(SchoolUnique schoolUq, ICourse course,
            CancellationToken cancellationToken = default)
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            return await FindUniqueAsync(schoolUq, course.Code,
                cancellationToken);
        }

        public async Task<Course?> FindUniqueAsync(CourseUnique courseUnique,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(courseUnique.SchoolUnique, courseUnique.Code,
                cancellationToken);
        }

        #endregion
    }
}
