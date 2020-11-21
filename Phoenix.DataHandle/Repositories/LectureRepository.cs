using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class LectureRepository : RepositoryAsync<Lecture>
    {
        public LectureRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override Task<Lecture> Create(Lecture tModel, CancellationToken cancellationToken = default)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.Create(tModel, cancellationToken);
        }

        public override Task<Lecture> Update(Lecture tModel, CancellationToken cancellationToken = default)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel, cancellationToken);
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
