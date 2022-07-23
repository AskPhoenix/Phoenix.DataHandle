﻿using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.DataEntry.Types.Uniques;
using Phoenix.DataHandle.Main.Models;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class CourseRepository : ObviableRepository<Course>
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
    }
}
