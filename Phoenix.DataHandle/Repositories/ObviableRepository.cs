﻿using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public abstract class ObviableRepository<TObviableModel> : Repository<TObviableModel> 
        where TObviableModel : class, IObviableModelEntity
    {
        public ObviableRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

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

        public TObviableModel Obviate(TObviableModel obviableModel)
        {
            return Update(ObviatePrepare(obviableModel));
        }

        public IEnumerable<TObviableModel> ObviateRange(IEnumerable<TObviableModel> obviableModels)
        {
            return UpdateRange(ObviateRangePrepare(obviableModels));
        }

        public async Task<TObviableModel> ObviateAsync(TObviableModel obviableModel,
            CancellationToken cancellationToken = default)
        {
            return await UpdateAsync(ObviatePrepare(obviableModel), cancellationToken);
        }

        public async Task<IEnumerable<TObviableModel>> ObviateRangeAsync(IEnumerable<TObviableModel> obviableModels,
            CancellationToken cancellationToken = default)
        {
            return await UpdateRangeAsync(ObviateRangePrepare(obviableModels), cancellationToken);
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

        public TObviableModel Restore(TObviableModel obviableModel)
        {
            return Update(RestorePrepare(obviableModel));
        }

        public IEnumerable<TObviableModel> RestoreRange(IEnumerable<TObviableModel> obviableModels)
        {
            return UpdateRange(RestoreRangePrepare(obviableModels));
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

        public void DeleteAllObviated()
        {
            DeleteRange(Find().Where(m => m.IsObviated));
        }

        public async Task DeleteAllObviatedAsync(CancellationToken cancellationToken = default)
        {
            await DeleteRangeAsync(Find().Where(m => m.IsObviated), cancellationToken);
        }

        #endregion
    }
}
