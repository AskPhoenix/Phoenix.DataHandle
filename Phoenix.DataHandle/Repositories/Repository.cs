using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models.Extensions;

namespace Phoenix.DataHandle.Repositories
{
    public class Repository<TModel> where TModel : class, IModelEntity
    {
        protected readonly DbContext dbContext;
        protected readonly ICollection<Func<IQueryable<TModel>, IQueryable<TModel>>> includes = new List<Func<IQueryable<TModel>, IQueryable<TModel>>>();

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual IQueryable<TModel> Find()
        {
            IQueryable<TModel> x = this.dbContext.Set<TModel>();

            if (this.includes != null)
                foreach (var include in this.includes)
                    x = include(x);

            return x;
        }

        public virtual Task<TModel> Find(int id)
        {
            IQueryable<TModel> x = this.dbContext.Set<TModel>();

            if (this.includes != null)
                foreach (var include in this.includes)
                    x = include(x);

            return x.SingleAsync(a => a.Id == id);
        }

        public virtual Task<TModel> Find(Expression<Func<TModel, bool>> checkUnique)
        {
            IQueryable<TModel> x = this.dbContext.Set<TModel>();

            if (this.includes != null)
                foreach (var include in this.includes)
                    x = include(x);

            return x.SingleOrDefaultAsync(checkUnique);
        }

        public virtual TModel Create(TModel tModel)
        {
            this.dbContext.Set<TModel>().Add(tModel);
            this.dbContext.SaveChanges();

            return tModel;
        }

        public virtual TModel Update(TModel tModel)
        {
            this.dbContext.Entry(tModel).State = EntityState.Modified;
            this.dbContext.SaveChanges();

            return tModel;
        }

        public virtual bool Delete(int id)
        {
            this.dbContext.Set<TModel>().Remove(this.dbContext.Set<TModel>().Single(a => a.Id == id));
            this.dbContext.SaveChanges();

            return true;
        }

        public virtual void Include(params Expression<Func<TModel, object>>[] paths)
        {
            foreach (var path in paths)
            {
                this.includes.Add(models => models.Include(path));
            }
        }

        public virtual void Include(params Func<IQueryable<TModel>, IQueryable<TModel>>[] includes)
        {
            foreach (var include in includes)
            {
                this.includes.Add(include);
            }
        }

        public virtual void IncludeClear()
        {
            this.includes.Clear();
        }
    }
}
