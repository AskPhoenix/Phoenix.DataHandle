using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.DataEntry.Types.Uniques;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories.Extensions;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class SchoolRepository : ObviableRepository<School>,
        ISetNullDeleteRule<School>, ICascadeDeleteRule<School>
    {
        public SchoolRepository(PhoenixContext dbContext) 
            : base(dbContext) 
        {
            Include(s => s.SchoolSetting);
        }

        public SchoolRepository(PhoenixContext dbContext, bool nonObviatedOnly)
            : base(dbContext, nonObviatedOnly)
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

        public Task<School?> FindUniqueAsync(ISchoolBase school,
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

        // Always update SchoolSetting with School

        public override Task<School> UpdateAsync(School model,
            CancellationToken cancellationToken = default)
        {
            if (model is not null && model.SchoolSetting is not null)
                this.DbContext.Entry(model.SchoolSetting).State = EntityState.Modified;

            return base.UpdateAsync(model!, cancellationToken);
        }

        public override Task<IEnumerable<School>> UpdateRangeAsync(IEnumerable<School> models,
            CancellationToken cancellationToken = default)
        {
            if (models is not null)
                foreach (var model in models)
                    if (model.SchoolSetting is not null)
                        this.DbContext.Entry(model.SchoolSetting).State = EntityState.Modified;

            return base.UpdateRangeAsync(models!, cancellationToken);
        }

        #endregion

        #region Delete

        public void SetNullOnDelete(School school)
        {
            school.Users.Clear();
        }

        public async Task CascadeOnDeleteAsync(School school,
            CancellationToken cancellationToken = default)
        {
            await new BroadcastRepository(DbContext).DeleteRangeAsync(school.Broadcasts, cancellationToken);
            await new ClassroomRepository(DbContext).DeleteRangeAsync(school.Classrooms, cancellationToken);
            await new CourseRepository(DbContext).DeleteRangeAsync(school.Courses, cancellationToken);
            await new SchoolConnectionRepository(DbContext).DeleteRangeAsync(school.SchoolConnections, cancellationToken);
        }

        public async Task CascadeRangeOnDeleteAsync(IEnumerable<School> schools,
            CancellationToken cancellationToken = default)
        {
            await new BroadcastRepository(DbContext).DeleteRangeAsync(schools.SelectMany(s => s.Broadcasts),
                cancellationToken);
            await new ClassroomRepository(DbContext).DeleteRangeAsync(schools.SelectMany(s => s.Classrooms),
                cancellationToken);
            await new CourseRepository(DbContext).DeleteRangeAsync(schools.SelectMany(s => s.Courses),
                cancellationToken);
            await new SchoolConnectionRepository(DbContext).DeleteRangeAsync(schools.SelectMany(s => s.SchoolConnections),
                cancellationToken);
        }

        #endregion
    }
}
