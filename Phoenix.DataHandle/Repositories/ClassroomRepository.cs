using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class ClassroomRepository : Repository<Classroom>
    {
        public ClassroomRepository(PhoenixContext dbContext) : base(dbContext) { }
    }
}
