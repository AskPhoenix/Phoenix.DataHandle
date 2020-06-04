using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;
using System.Linq;
using System.Threading.Tasks;
using Phoenix.DataHandle.Main.Models.Extensions;

namespace Phoenix.DataHandle.Main
{
    public class Repository<TModel> where TModel : class, IModelEntity
    {
        protected readonly DbContext dbContext;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual IQueryable<TModel> find()
        {
            return this.dbContext.Set<TModel>();
        }

        public virtual Task<TModel> find(int id)
        {
            return this.dbContext.Set<TModel>().SingleAsync(a => a.Id == id);
        }

        public virtual TModel create(TModel tModel)
        {
            this.dbContext.Set<TModel>().Add(tModel);
            this.dbContext.SaveChanges();

            return tModel;
        }

        public virtual TModel update(TModel tModel)
        {
            this.dbContext.Entry(tModel).State = EntityState.Modified;
            this.dbContext.SaveChanges();

            return tModel;
        }

        public virtual bool delete(int id)
        {
            this.dbContext.Set<TModel>().Remove(this.dbContext.Set<TModel>().Single(a => a.Id == id));
            this.dbContext.SaveChanges();

            return true;
        }

    }
}
