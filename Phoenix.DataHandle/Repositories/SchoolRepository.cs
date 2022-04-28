using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class SchoolRepository : ObviableRepository<School>
    {
        public SchoolRepository(PhoenixContext dbContext) 
            : base(dbContext) 
        {
            Include(s => s.SchoolSetting);
        }

        public static Expression<Func<School, bool>> GetUniqueExpression(int schoolCode)
        {
            return s => s.Code == schoolCode;
        }

        #region Find Unique

        public Task<School?> FindUniqueAsync(int schoolCode,
            CancellationToken cancellationToken = default)
        {
            return FindUniqueAsync(GetUniqueExpression(schoolCode), cancellationToken);
        }

        public Task<School?> FindUniqueAsync(ISchool school,
            CancellationToken cancellationToken = default)
        {
            if (school is null)
                throw new ArgumentNullException(nameof(school));

            return FindUniqueAsync(school.Code, cancellationToken);
        }

        public Task<School?> FindUniqueAsync(SchoolUnique schoolUnique,
            CancellationToken cancellationToken = default)
        {
            if (schoolUnique is null)
                throw new ArgumentNullException(nameof(schoolUnique));

            return FindUniqueAsync(schoolUnique.Code, cancellationToken);
        }

        #endregion

        #region Update

        public Task<School> UpdateWithSchoolSettingAsync(School school, ISchool schoolFrom,
            CancellationToken cancellationToken = default)
        {
            if (school is null)
                throw new ArgumentNullException(nameof(school));
            if (schoolFrom is null)
                throw new ArgumentNullException(nameof(schoolFrom));
            
            if (schoolFrom.SchoolSetting is null)
                throw new ArgumentNullException(nameof(schoolFrom.SchoolSetting));
            if (school.SchoolSetting is null)
                school.SchoolSetting = new();

            PropertyCopier.CopyFromBase(school.SchoolSetting, schoolFrom.SchoolSetting);

            return UpdateAsync(school, schoolFrom, cancellationToken);
        }

        #endregion
    }
}
