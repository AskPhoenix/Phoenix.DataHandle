using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class SchoolRepository : Repository<School>
    {
        public SchoolRepository(PhoenixContext dbContext) : base(dbContext)
        {
        }

        public override School create(School tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.create(tModel);
        }

        public override School update(School tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.update(tModel);
        }


        public IQueryable<Course> FindCourses(int id)
        {
            this.include(a => a.Course);
            return this.find().Where(a => a.Id == id).SelectMany(a => a.Course);
        }

        public IQueryable<Classroom> FindClassrooms(int id)
        {
            this.include(a => a.Classroom);
            return this.find().Where(a => a.Id == id).SelectMany(a => a.Classroom);
        }

    }
}
