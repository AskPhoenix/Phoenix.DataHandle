using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public class ObviableRepository<TObviableModel> : Repository<TObviableModel> where TObviableModel : class, IObviableModelEntity
    {
        public ObviableRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public virtual TObviableModel Obviate(TObviableModel obviableModel)
        {
            if (obviableModel == null)
                throw new ArgumentNullException(nameof(obviableModel));

            if (obviableModel.IsObviated)
                return obviableModel;

            obviableModel.ObviatedAt = DateTimeOffset.Now;

            return this.Update(obviableModel);
        }

        public virtual async Task<TObviableModel> ObviateAsync(TObviableModel obviableModel, CancellationToken cancellationToken = default)
        {
            if (obviableModel == null)
                throw new ArgumentNullException(nameof(obviableModel));

            if (obviableModel.IsObviated)
                return obviableModel;

            obviableModel.ObviatedAt = DateTimeOffset.Now;

            return await this.UpdateAsync(obviableModel, cancellationToken);
        }

        public virtual TObviableModel Restore(TObviableModel obviableModel)
        {
            if (obviableModel == null)
                throw new ArgumentNullException(nameof(obviableModel));

            if (!obviableModel.IsObviated)
                return obviableModel;

            obviableModel.ObviatedAt = null;

            return this.Update(obviableModel);
        }

        public virtual async Task<TObviableModel> RestoreAsync(TObviableModel obviableModel, CancellationToken cancellationToken = default)
        {
            if (obviableModel == null)
                throw new ArgumentNullException(nameof(obviableModel));

            if (!obviableModel.IsObviated)
                return obviableModel;

            obviableModel.ObviatedAt = null;

            return await this.UpdateAsync(obviableModel, cancellationToken);
        }

        public virtual void RemoveAllObviated()
        {
            IQueryable<TObviableModel> toRemove = this.Find().Where(a => a.IsObviated);

            this.dbContext.Set<TObviableModel>().RemoveRange(toRemove);
            this.dbContext.SaveChanges();
        }

        public virtual async Task RemoveAllObviatedAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<TObviableModel> toRemove = this.Find().Where(a => a.IsObviated);

            this.dbContext.Set<TObviableModel>().RemoveRange(toRemove);
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
