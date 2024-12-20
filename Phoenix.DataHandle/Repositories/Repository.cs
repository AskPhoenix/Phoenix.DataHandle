﻿using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Models.Extensions;
using Phoenix.DataHandle.Repositories.Extensions;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public abstract class Repository<TModel> : IDisposable
        where TModel : class, IModelEntity
    {
        protected PhoenixContext DbContext { get; }
        protected DbSet<TModel> Set => this.DbContext.Set<TModel>();
        protected ICollection<Func<IQueryable<TModel>, IQueryable<TModel>>> Includes { get; }

        public Repository(PhoenixContext dbContext)
        {
            this.DbContext = dbContext;
            this.Includes = new List<Func<IQueryable<TModel>, IQueryable<TModel>>>();
        }

        public void Dispose()
        {
            IncludeClear();
            this.DbContext.Dispose();

            GC.SuppressFinalize(this);
        }

        #region Find

        public IQueryable<TModel> Find()
        {
            IQueryable<TModel> q = this.Set;

            foreach (var include in this.Includes)
                q = include(q);

            return q;
        }

        public Task<TModel?> FindPrimaryAsync(int id,
            CancellationToken cancellationToken = default)
        {
            return Find()
                .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        protected Task<TModel?> FindUniqueAsync(Expression<Func<TModel, bool>> unique,
            CancellationToken cancellationToken = default)
        {
            return Find()
                .SingleOrDefaultAsync(unique, cancellationToken);
        }

        #endregion

        #region Create

        private TModel CreatePrepare(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            model.CreatedAt = DateTime.UtcNow;

            if (typeof(TModel) is INormalizableEntity<TModel>)
                ((INormalizableEntity<TModel>)model).Normalize();

            return model;
        }

        private IEnumerable<TModel> CreateRangePrepare(IEnumerable<TModel> models)
        {
            if (models is null)
                throw new ArgumentNullException(nameof(models));

            foreach (var model in models)
                CreatePrepare(model);

            return models;
        }

        public virtual async Task<TModel> CreateAsync(TModel model,
            CancellationToken cancellationToken = default)
        {
            CreatePrepare(model);

            await this.Set.AddAsync(model, cancellationToken);
            await this.DbContext.SaveChangesAsync(cancellationToken);

            return model;
        }

        public virtual async Task<IEnumerable<TModel>> CreateRangeAsync(IEnumerable<TModel> models,
            CancellationToken cancellationToken = default)
        {
            var createdModels = CreateRangePrepare(models);
            if (!createdModels.Any())
                return Enumerable.Empty<TModel>();

            await this.Set.AddRangeAsync(createdModels, cancellationToken);
            await this.DbContext.SaveChangesAsync(cancellationToken);

            return createdModels;
        }

        #endregion

        #region Update

        private TModel UpdatePrepare(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            model.UpdatedAt = DateTime.UtcNow;

            if (typeof(TModel) is INormalizableEntity<TModel>)
                ((INormalizableEntity<TModel>)model).Normalize();

            this.DbContext.Entry(model).State = EntityState.Modified;

            return model;
        }

        private IEnumerable<TModel> UpdateRangePrepare(IEnumerable<TModel> models)
        {
            if (models is null)
                throw new ArgumentNullException(nameof(models));

            foreach (var model in models)
                UpdatePrepare(model);

            return models;
        }

        public virtual async Task<TModel> UpdateAsync(TModel model,
            CancellationToken cancellationToken = default)
        {
            UpdatePrepare(model);

            await this.DbContext.SaveChangesAsync(cancellationToken);

            return model;
        }

        public virtual async Task<IEnumerable<TModel>> UpdateRangeAsync(IEnumerable<TModel> models,
            CancellationToken cancellationToken = default)
        {
            UpdateRangePrepare(models);

            if (!models.Any())
                return Enumerable.Empty<TModel>();

            await this.DbContext.SaveChangesAsync(cancellationToken);

            return models;
        }
        #endregion

        #region Delete

        private async Task<TModel> DeletePrepareAsync(TModel model,
            CancellationToken cancellationToken = default)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            if (this is ISetNullDeleteRule<TModel> setNullRuleRepository)
                setNullRuleRepository.SetNullOnDelete(model);

            if (this is ICascadeDeleteRule<TModel> cascadeRuleRepository)
                await cascadeRuleRepository.CascadeOnDeleteAsync(model, cancellationToken);
                

            // This is included here because there is no async remove method
            this.Set.Remove(model);

            return model;
        }

        private async Task<IEnumerable<TModel>> DeleteRangePrepareAsync(IEnumerable<TModel> models,
            CancellationToken cancellationToken = default)
        {
            if (models is null)
                throw new ArgumentNullException(nameof(models));
            if (!models.Any())
                return Enumerable.Empty<TModel>();

            if (this is ISetNullDeleteRule<TModel> setNullRuleRepository)
                for (int i = 0; i < models.Count(); i++)
                    setNullRuleRepository.SetNullOnDelete(models.ElementAt(i));

            if (this is ICascadeDeleteRule<TModel> cascadeRuleRepository)
                await cascadeRuleRepository.CascadeRangeOnDeleteAsync(models, cancellationToken);

            this.Set.RemoveRange(models);

            return models;
        }

        private async Task<IEnumerable<TModel>> DeleteRangePrepareAsync(IEnumerable<int> ids,
            CancellationToken cancellationToken = default)
        {
            if (ids is null)
                throw new ArgumentNullException(nameof(ids));
            if (!ids.Any())
                return Enumerable.Empty<TModel>();

            return await DeleteRangePrepareAsync(Find().Where(m => ids.Contains(m.Id)),
                cancellationToken);
        }

        public virtual async Task<TModel> DeleteAsync(TModel model,
            CancellationToken cancellationToken = default)
        {
            await DeletePrepareAsync(model, cancellationToken);

            await this.DbContext.SaveChangesAsync(cancellationToken);

            return model;
        }

        public virtual async Task<TModel> DeleteAsync(int id,
            CancellationToken cancellationToken = default)
        {
            TModel? toRemove = await FindPrimaryAsync(id, cancellationToken);
            if (toRemove is null)
                throw new InvalidOperationException($"There is no entry with id {id}.");

            return await DeleteAsync(toRemove, cancellationToken);
        }

        public virtual async Task<IEnumerable<TModel>> DeleteRangeAsync(IEnumerable<TModel> models,
            CancellationToken cancellationToken = default)
        {
             await DeleteRangePrepareAsync(models, cancellationToken);

            await this.DbContext.SaveChangesAsync(cancellationToken);

            return models;
        }

        public virtual async Task<IEnumerable<TModel>> DeleteRangeAsync(IEnumerable<int> ids,
            CancellationToken cancellationToken = default)
        {
            return await DeleteRangeAsync(await DeleteRangePrepareAsync(ids, cancellationToken),
                cancellationToken);
        }

        #endregion

        #region Include

        public void Include(params Expression<Func<TModel, object>>[] paths)
        {
            if (paths is null)
                throw new ArgumentNullException(nameof(paths));

            foreach (var path in paths)
                this.Includes.Add(models => models.Include(path));
        }

        public void Include(params Func<IQueryable<TModel>, IQueryable<TModel>>[] includes)
        {
            if (includes is null)
                throw new ArgumentNullException(nameof(includes));

            foreach (var include in includes)
                this.Includes.Add(include);
        }

        public void IncludeClear()
        {
            this.Includes.Clear();
        }

        #endregion
    }
}
