using Phoenix.DataHandle.DataEntry.Types.Uniques;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class BroadcastRepository : Repository<Broadcast>
    {
        public BroadcastRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        #region Search

        public IQueryable<Broadcast> Search(DateTime scheduledFor, int? authorId = null, int? schoolId = null)
        {
            var broadcasts = Find().Where(b => b.ScheduledFor.Date == scheduledFor.Date);

            if (authorId.HasValue)
                broadcasts = broadcasts.Where(b => b.AuthorId == authorId);

            if (schoolId.HasValue)
                broadcasts = broadcasts.Where(b => b.SchoolId == schoolId);

            return broadcasts;
        }

        public IQueryable<Broadcast> Search(DateTime scheduledFor, SchoolUnique schoolUq)
        {
            if (schoolUq is null)
                throw new ArgumentNullException(nameof(schoolUq));

            return Find().Where(b => b.School.Code == schoolUq.Code && b.ScheduledFor.Date == scheduledFor.Date);
        }

        public IQueryable<Broadcast> Search(DateTime scheduledFor, Daypart daypart)
        {
            return Find().Where(b => b.ScheduledFor.Date == scheduledFor.Date && b.Daypart == daypart);
        }

        #endregion
    }
}
