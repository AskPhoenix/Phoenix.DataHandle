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
    public class Repository<TModel> where TModel : class, IModelEntity
    {
        protected DbContext dbContext { get; }
        protected ICollection<Func<IQueryable<TModel>, IQueryable<TModel>>> includes { get; }

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.includes = new List<Func<IQueryable<TModel>, IQueryable<TModel>>>();
        }

        public virtual IQueryable<TModel> Find()
        {
            IQueryable<TModel> x = this.dbContext.Set<TModel>();
            var kati = dbContext.Set<TModel>();

            foreach (var include in this.includes)
                x = include(x);

            return x;
        }

        public virtual TModel Find(int id)
        {
            return this.Find().Single(a => a.Id == id);
        }

        public virtual TModel Find(Expression<Func<TModel, bool>> unique)
        {
            return this.Find().SingleOrDefault(unique);
        }

        public virtual Task<TModel> FindAsync(int id, CancellationToken cancellationToken = default)
        {
            return this.Find().SingleAsync(a => a.Id == id, cancellationToken);
        }

        public virtual Task<TModel> FindAsync(Expression<Func<TModel, bool>> unique, CancellationToken cancellationToken = default)
        {
            return this.Find().SingleOrDefaultAsync(unique, cancellationToken);
        }

        private static void create(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.CreatedAt = DateTimeOffset.UtcNow;
        }

        public virtual TModel Create(TModel model)
        {
            create(model);

            this.dbContext.Set<TModel>().Add(model);
            this.dbContext.SaveChanges();

            return model;
        }

        public virtual async Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            create(model);

            await this.dbContext.Set<TModel>().AddAsync(model, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return model;
        }

        private static void update(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.UpdatedAt = DateTimeOffset.UtcNow;
        }

        public virtual TModel Update(TModel model)
        {
            update(model);

            this.dbContext.Entry(model).State = EntityState.Modified;
            this.dbContext.SaveChanges();

            return model;
        }

        public virtual async Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            update(model);

            this.dbContext.Entry(model).State = EntityState.Modified;
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return model;
        }

        public virtual TModel Delete(TModel model)
        {
            this.dbContext.Set<TModel>().Remove(model);
            this.dbContext.SaveChanges();

            return model;
        }

        public virtual TModel Delete(int id)
        {
            TModel toRemove = this.Find(id);

            return this.Delete(toRemove);
        }

        public virtual async Task<TModel> DeleteAsync(TModel model, CancellationToken cancellationToken = default)
        {
            this.dbContext.Set<TModel>().Remove(model);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return model;
        }

        public virtual async Task<TModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            TModel toRemove = await this.FindAsync(id, cancellationToken);

            return await this.DeleteAsync(toRemove, cancellationToken);
        }

        public virtual void Include(params Expression<Func<TModel, object>>[] paths)
        {
            if (paths is null)
                throw new ArgumentNullException(nameof(paths));

            foreach (var path in paths)
                this.includes.Add(models => models.Include(path));
        }

        public virtual void Include(params Func<IQueryable<TModel>, IQueryable<TModel>>[] includes)
        {
            if (includes is null)
                throw new ArgumentNullException(nameof(includes));

            foreach (var include in includes)
                this.includes.Add(include);
        }

        public virtual void IncludeClear()
        {
            this.includes.Clear();
        }
    }
}
