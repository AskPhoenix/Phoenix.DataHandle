using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class SchoolRepository : ObviableRepository<School>
    {
        public SchoolRepository(PhoenixContext dbContext) 
            : base(dbContext) 
        {
            Include(s => s.SchoolInfo);
        }

        public static Expression<Func<School, bool>> GetUniqueExpression(int schoolCode)
        {
            return s => s.Code == schoolCode;
        }

        #region Find Unique

        public School? FindUnique(int schoolCode)
        {
            return FindUnique(GetUniqueExpression(schoolCode));
        }

        public School? FindUnique(ISchool school)
        {
            if (school is null)
                throw new ArgumentNullException(nameof(school));

            return FindUnique(school.Code);
        }

        public School? FindUnique(SchoolUnique schoolUnique)
        {
            if (schoolUnique is null)
                throw new ArgumentNullException(nameof(schoolUnique));

            return FindUnique(schoolUnique.Code);
        }

        public async Task<School?> FindUniqueAsync(int schoolCode,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(schoolCode), cancellationToken);
        }

        public async Task<School?> FindUniqueAsync(ISchool school,
            CancellationToken cancellationToken = default)
        {
            if (school is null)
                throw new ArgumentNullException(nameof(school));

            return await FindUniqueAsync(school.Code, cancellationToken);
        }

        public async Task<School?> FindUniqueAsync(SchoolUnique schoolUnique,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(schoolUnique.Code, cancellationToken);
        }

        #endregion

        #region Update

        public School UpdateWithSchoolInfo(School school, ISchool schoolFrom)
        {
            CopySchoolInfo(school.SchoolInfo, schoolFrom.SchoolInfo);
            return Update(school, schoolFrom);
        }

        public async Task<School> UpdateWithSchoolInfoAsync(School school, ISchool schoolFrom,
            CancellationToken cancellationToken = default)
        {
            CopySchoolInfo(school.SchoolInfo, schoolFrom.SchoolInfo);
            return await UpdateAsync(school, schoolFrom, cancellationToken);
        }

        public SchoolInfo CopySchoolInfo(SchoolInfo schoolInfo, ISchoolInfo schoolInfoFrom)
        {
            if (schoolInfoFrom is null)
                throw new ArgumentNullException(nameof(schoolInfoFrom));
            if (schoolInfo is null)
                schoolInfo = new();

            PropertyCopier.CopyFromBase(schoolInfo, schoolInfoFrom);

            return schoolInfo;
        }

        #endregion
    }
}
