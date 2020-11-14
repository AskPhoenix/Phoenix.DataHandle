using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class LectureRepository : Repository<Lecture>
    {
        public LectureRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override Lecture Create(Lecture tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.Create(tModel);
        }

        public override Lecture Update(Lecture tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }

        public Task<Lecture> FindSingle(int courseId, DateTime day, TimeSpan time, CancellationToken cancellationToken)
        {
            return this
                .Find()
                .Where(a => a.CourseId == courseId)
                .Where(a => a.StartDateTime.Date == day)
                .SingleOrDefaultAsync(a => a.StartDateTime.TimeOfDay == time, cancellationToken: cancellationToken);
        }
    }
}
