using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DataHandle
{
    public class Repository<TModel> where TModel : class, IModelDb
    {
        protected readonly DbContext dbContext;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual IQueryable<TModel> find()
        {
            return dbContext.Set<TModel>();
        }

        public virtual TModel find(int id)
        {
            return dbContext.Set<TModel>().Single(a => a.id == id);
        }

        public virtual TModel create(TModel tModel)
        {
            dbContext.Set<TModel>().Add(tModel);
            dbContext.SaveChanges();

            return tModel;
        }

        public virtual TModel update(TModel tModel)
        {
            dbContext.Entry(tModel).State = EntityState.Modified;
            dbContext.SaveChanges();

            return tModel;
        }

        public virtual bool delete(int id)
        {
            dbContext.Set<TModel>().Remove(dbContext.Set<TModel>().Single(a => a.id == id));
            dbContext.SaveChanges();

            return true;
        }

    }
}
