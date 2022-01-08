using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.DataHandle.Repositories
{
    public class ObviableRepository<TObviableModel> : Repository<TObviableModel>
        where TObviableModel : class, IObviableModelEntity
    {
        public ObviableRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public virtual TObviableModel Obviate(TObviableModel tObviableModel)
        {
            if (tObviableModel == null)
                throw new ArgumentNullException(nameof(tObviableModel));

            tObviableModel.IsObviated = true;
            tObviableModel.ObviatedAt = DateTimeOffset.Now;

            return this.Update(tObviableModel);
        }
    }
}
