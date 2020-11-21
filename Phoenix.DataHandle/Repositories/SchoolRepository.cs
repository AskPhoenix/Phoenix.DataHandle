using System;
using System.Linq;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class SchoolRepository : Repository<School>
    {
        public SchoolRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override School Create(School tModel)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.Create(tModel);
        }

        public override School Update(School tModel)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }

        public School Update(School tModel, School tModelFrom)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            if (tModelFrom == null)
                throw new ArgumentNullException(nameof(tModelFrom));

            tModel.Slug = tModelFrom.Slug;
            tModel.AddressLine = tModelFrom.AddressLine;
            tModel.Info = tModelFrom.Info;

            return this.Update(tModel);
        }

        public IQueryable<Course> FindCourses(int id)
        {
            this.Include(a => a.Course);
            return this.Find().Where(a => a.Id == id).SelectMany(a => a.Course);
        }

        public IQueryable<Classroom> FindClassrooms(int id)
        {
            this.Include(a => a.Classroom);
            return this.Find().Where(a => a.Id == id).SelectMany(a => a.Classroom);
        }
    }
}
