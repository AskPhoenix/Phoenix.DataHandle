using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class ClassroomRepository : ObviableRepository<Classroom>
    {
        public ClassroomRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
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
            if (string.IsNullOrWhiteSpace(classroomName))
                throw new ArgumentNullException(nameof(classroomName));

            string normName = Classroom.NormFunc(classroomName);
            return c => c.School.Code == schoolUq.Code && c.NormalizedName == normName;
        }

        #region Find Unique

        public Classroom? FindUnique(int schoolId, string classroomName)
        {
            return FindUnique(GetUniqueExpression(schoolId, classroomName));
        }

        public Classroom? FindUnique(int schoolId, IClassroom classroom)
        {
            if (classroom is null)
                throw new ArgumentNullException(nameof(classroom));

            return FindUnique(schoolId, classroom.Name);
        }

        public Classroom? FindUnique(SchoolUnique schoolUq, string classroomName)
        {
            return FindUnique(GetUniqueExpression(schoolUq, classroomName));
        }

        public Classroom? FindUnique(SchoolUnique schoolUq, IClassroom classroom)
        {
            if (classroom is null)
                throw new ArgumentNullException(nameof(classroom));

            return FindUnique(schoolUq, classroom.Name);
        }

        public async Task<Classroom?> FindUniqueAsync(int schoolId, string classroomName,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(schoolId, classroomName),
                cancellationToken);
        }

        public async Task<Classroom?> FindUniqueAsync(int schoolId, IClassroom classroom,
            CancellationToken cancellationToken = default)
        {
            if (classroom is null)
                throw new ArgumentNullException(nameof(classroom));

            return await FindUniqueAsync(schoolId, classroom.Name,
                cancellationToken);
        }

        public async Task<Classroom?> FindUniqueAsync(SchoolUnique schoolUq, string classroomName,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(schoolUq, classroomName),
                cancellationToken);
        }

        public async Task<Classroom?> FindUniqueAsync(SchoolUnique schoolUq, IClassroom classroom,
            CancellationToken cancellationToken = default)
        {
            if (classroom is null)
                throw new ArgumentNullException(nameof(classroom));

            return await FindUniqueAsync(schoolUq, classroom.Name,
                cancellationToken);
        }

        #endregion
    }
}
