using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(PhoenixContext dbContext) : base(dbContext)
        {
        }

        public override Course create(Course tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.create(tModel);
        }

        public override Course update(Course tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.update(tModel);
        }
        

    }
}
