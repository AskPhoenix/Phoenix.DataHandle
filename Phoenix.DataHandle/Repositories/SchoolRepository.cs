using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class SchoolRepository : Repository<School>
    {
        public SchoolRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override School create(School tModel)
        {
            tModel.CreatedAt = DateTime.Now;
            
            return base.create(tModel);
        }

        public override School update(School tModel)
        {
            tModel.UpdatedAt = DateTime.Now;

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
