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
            return dbContext.Set<TModel>();
        }

        public virtual Task<TModel> find(int id)
        {
            return dbContext.Set<TModel>().SingleAsync(a => a.Id == id);
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
            dbContext.Set<TModel>().Remove(dbContext.Set<TModel>().Single(a => a.Id == id));
            dbContext.SaveChanges();

            return true;
        }

    }
}
