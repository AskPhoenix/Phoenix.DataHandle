using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class BroadcastRepository : Repository<Broadcast>
    {
        public BroadcastRepository(PhoenixContext dbContext) : base(dbContext) { }
    }
}
