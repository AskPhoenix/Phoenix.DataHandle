using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models.Extensions;

namespace Phoenix.DataHandle.Repositories
{
    public abstract class ObviableRepository<TObviableModel> : Repository<TObviableModel> 
        where TObviableModel : class, IObviableModelEntity
    {
        public bool NonObviatedOnly { get; set; }

        public ObviableRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public ObviableRepository(DbContext dbContext, bool nonObviatedOnly)
            : this(dbContext)
        {
            this.NonObviatedOnly = nonObviatedOnly;
        }

        #region Find

        public new IQueryable<TObviableModel> Find()
        {
            var q = base.Find();

            if (this.NonObviatedOnly)
                return q.Where(m => !m.IsObviated);

            return q;
        }

        #endregion

        #region Obviate

        private TObviableModel ObviatePrepare(TObviableModel obviableModel)
        {
            if (obviableModel is null)
                throw new ArgumentNullException(nameof(obviableModel));

            if (obviableModel.IsObviated)
                return obviableModel;

            obviableModel.ObviatedAt = DateTime.UtcNow;

            return obviableModel;
        }

        private IEnumerable<TObviableModel> ObviateRangePrepare(IEnumerable<TObviableModel> obviableModels)
        {
            if (obviableModels is null)
                throw new ArgumentNullException(nameof(obviableModels));

            return obviableModels.Select(m => ObviatePrepare(m));
        }

        public Task<TObviableModel> ObviateAsync(TObviableModel obviableModel,
            CancellationToken cancellationToken = default)
        {
            return UpdateAsync(ObviatePrepare(obviableModel), cancellationToken);
        }

        public Task<IEnumerable<TObviableModel>> ObviateRangeAsync(IEnumerable<TObviableModel> obviableModels,
            CancellationToken cancellationToken = default)
        {
            return UpdateRangeAsync(ObviateRangePrepare(obviableModels), cancellationToken);
        }

        #endregion

        #region Restore

        private TObviableModel RestorePrepare(TObviableModel obviableModel)
        {
            if (obviableModel is null)
                throw new ArgumentNullException(nameof(obviableModel));

            obviableModel.ObviatedAt = null;

            return obviableModel;
        }

        private IEnumerable<TObviableModel> RestoreRangePrepare(IEnumerable<TObviableModel> obviableModels)
        {
            if (obviableModels is null)
                throw new ArgumentNullException(nameof(obviableModels));

            return obviableModels.Select(m => RestorePrepare(m));
        }

        public async Task<TObviableModel> RestoreAsync(TObviableModel obviableModel,
            CancellationToken cancellationToken = default)
        {
            return await UpdateAsync(RestorePrepare(obviableModel), cancellationToken);
        }

        public async Task<IEnumerable<TObviableModel>> RestoreRangeAsync(IEnumerable<TObviableModel> obviableModels,
            CancellationToken cancellationToken = default)
        {
            return await UpdateRangeAsync(RestoreRangePrepare(obviableModels), cancellationToken);
        }

        #endregion

        #region Delete

        public Task DeleteAllObviatedAsync(CancellationToken cancellationToken = default)
        {
            return DeleteRangeAsync(Find().Where(m => m.IsObviated), cancellationToken);
        }

        #endregion
    }
}
