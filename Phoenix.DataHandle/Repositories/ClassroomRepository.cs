using Phoenix.DataHandle.Main.Models;
using System;
using System.Linq;

namespace Phoenix.DataHandle.Repositories
{
    public class ClassroomRepository : ObviableRepository<Classroom>
    {
        public ClassroomRepository(PhoenixContext dbContext) : base(dbContext) { }

        public Classroom Find(int schoolId, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return this.Find().SingleOrDefault(c => c.SchoolId == schoolId && c.NormalizedName == name.ToUpperInvariant());
        }

        public override Classroom Create(Classroom classroom)
        {
            if (classroom == null)
                throw new ArgumentNullException(nameof(classroom));

            classroom.NormalizedName = classroom.Name.ToUpperInvariant();

            return base.Create(classroom);
        }

        public override Classroom Update(Classroom classroom)
        {
            if (classroom == null)
                throw new ArgumentNullException(nameof(classroom));

            classroom.NormalizedName = classroom.Name.ToUpperInvariant();
            
            return base.Update(classroom);
        }
    }
}
