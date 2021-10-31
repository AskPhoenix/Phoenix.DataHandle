using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using System;
using System.Linq;

namespace Phoenix.DataHandle.Repositories
{
    public class BroadcastRepository : Repository<Broadcast>
    {
        public BroadcastRepository(PhoenixContext dbContext) : base(dbContext) { }

        public IQueryable<Broadcast> FindForDate(DateTime date)
        {
            return this.Find().Where(b => b.ScheduledDate.Date == date);
        }

        public IQueryable<Broadcast> FindForDateDaypart(DateTime date, Daypart daypart)
        {
            return this.FindForDate(date).Where(b => b.Daypart == daypart);
        }
    }
}
