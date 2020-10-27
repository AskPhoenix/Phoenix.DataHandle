using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class LectureRepository : Repository<Lecture>
    {
        public LectureRepository(PhoenixContext dbContext) : base(dbContext)
        {
        }

        public override Lecture create(Lecture tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.create(tModel);
        }

        public override Lecture update(Lecture tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.update(tModel);
        }

        public Task<Lecture> findSingle(int courseId, DateTime day, TimeSpan time, CancellationToken cancellationToken)
        {
            return this
                .find()
                .Where(a => a.CourseId == courseId)
                .Where(a => a.StartDateTime.Date == day)
                .SingleOrDefaultAsync(a => a.StartDateTime.TimeOfDay == time, cancellationToken: cancellationToken);
        }


    }
}
