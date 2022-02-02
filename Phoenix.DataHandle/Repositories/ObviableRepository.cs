using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models.Extensions;
using System;

namespace Phoenix.DataHandle.Repositories
{
    public class ObviableRepository<TObviableModel> : Repository<TObviableModel>
        where TObviableModel : class, IObviableModelEntity
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

        public virtual TObviableModel Restore(TObviableModel obviableModel)
        {
            if (obviableModel == null)
                throw new ArgumentNullException(nameof(obviableModel));

            if (!obviableModel.IsObviated)
                return obviableModel;

            obviableModel.ObviatedAt = null;

            return this.Update(obviableModel);
        }
    }
}
