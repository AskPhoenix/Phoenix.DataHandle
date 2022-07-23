using Phoenix.DataHandle.DataEntry.Types.Uniques;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class ClassroomRepository : ObviableRepository<Classroom>
    {
        public ClassroomRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        public ClassroomRepository(PhoenixContext phoenixContext, bool nonObviatedOnly)
            : base(phoenixContext, nonObviatedOnly)
        {
        }

        public static Expression<Func<Classroom, bool>> GetUniqueExpression(
            int schoolId, string classroomName)
        {
            if (string.IsNullOrWhiteSpace(classroomName))
                throw new ArgumentNullException(nameof(classroomName));

            string normName = Classroom.NormFunc(classroomName);
            return c => c.SchoolId == schoolId && c.NormalizedName == normName;
        }

        public static Expression<Func<Classroom, bool>> GetUniqueExpression(
            SchoolUnique schoolUq, string classroomName)
        {
            if (schoolUq is null)
                throw new ArgumentNullException(nameof(schoolUq));
            if (string.IsNullOrWhiteSpace(classroomName))
                throw new ArgumentNullException(nameof(classroomName));

            string normName = Classroom.NormFunc(classroomName);
            return c => c.School.Code == schoolUq.Code && c.NormalizedName == normName;
        }

        #region Find Unique

        public Task<Classroom?> FindUniqueAsync(int schoolId, string classroomName,
            CancellationToken cancellationToken = default)
        {
            return FindUniqueAsync(GetUniqueExpression(schoolId, classroomName),
                cancellationToken);
        }

        public Task<Classroom?> FindUniqueAsync(int schoolId, IClassroom classroom,
            CancellationToken cancellationToken = default)
        {
            if (classroom is null)
                throw new ArgumentNullException(nameof(classroom));

            return FindUniqueAsync(schoolId, classroom.Name,
                cancellationToken);
        }

        public Task<Classroom?> FindUniqueAsync(SchoolUnique schoolUq, string classroomName,
            CancellationToken cancellationToken = default)
        {
            return FindUniqueAsync(GetUniqueExpression(schoolUq, classroomName),
                cancellationToken);
        }

        public Task<Classroom?> FindUniqueAsync(SchoolUnique schoolUq, IClassroom classroom,
            CancellationToken cancellationToken = default)
        {
            if (classroom is null)
                throw new ArgumentNullException(nameof(classroom));

            return FindUniqueAsync(schoolUq, classroom.Name,
                cancellationToken);
        }

        #endregion
    }
}
