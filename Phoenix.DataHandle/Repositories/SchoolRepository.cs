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
            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.Create(tModel);
        }

        public override School Update(School tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
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
