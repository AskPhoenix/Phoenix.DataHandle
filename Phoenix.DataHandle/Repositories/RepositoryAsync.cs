using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models.Extensions;

namespace Phoenix.DataHandle.Repositories
{
    public class RepositoryAsync<TModel> where TModel : class, IModelEntity
    {
        protected DbContext dbContext { get; }
        protected ICollection<Func<IQueryable<TModel>, IQueryable<TModel>>> includes { get; }

        public RepositoryAsync(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.includes = new List<Func<IQueryable<TModel>, IQueryable<TModel>>>();
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

        public virtual async Task<TModel> Create(TModel tModel, CancellationToken cancellationToken = default)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.CreatedAt = DateTimeOffset.Now;

            this.dbContext.Set<TModel>().Add(tModel);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return tModel;
        }

        public virtual async Task<TModel> Update(TModel tModel, CancellationToken cancellationToken = default)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.UpdatedAt = DateTimeOffset.Now;

            this.dbContext.Entry(tModel).State = EntityState.Modified;
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return tModel;
        }

        public virtual async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            this.dbContext.Set<TModel>().Remove(this.dbContext.Set<TModel>().Single(a => a.Id == id));
            await this.dbContext.SaveChangesAsync(cancellationToken);

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
