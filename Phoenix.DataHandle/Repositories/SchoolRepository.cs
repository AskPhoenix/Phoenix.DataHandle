using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
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
    }
}
