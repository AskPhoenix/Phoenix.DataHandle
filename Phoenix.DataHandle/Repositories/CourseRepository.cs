using System;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override Course Create(Course tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.Create(tModel);
        }

        public override Course Update(Course tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }
    }
}
