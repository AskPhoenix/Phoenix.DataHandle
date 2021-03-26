using System;
using System.Linq;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class SchoolRepository : Repository<School>
    {
        public SchoolRepository(PhoenixContext dbContext) : base(dbContext) { }

        public School Update(School tModel, School tModelFrom)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (tModelFrom == null)
                throw new ArgumentNullException(nameof(tModelFrom));

            tModel.Name = tModelFrom.Name;
            tModel.Slug = tModelFrom.Slug;
            tModel.City = tModelFrom.City;
            tModel.AddressLine = tModelFrom.AddressLine;
            tModel.Info = tModelFrom.Info;

            //The columns of the unique keys should not be copied

            if (!string.IsNullOrWhiteSpace(tModelFrom.FacebookPageId))
                tModel.FacebookPageId = tModelFrom.FacebookPageId;

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
