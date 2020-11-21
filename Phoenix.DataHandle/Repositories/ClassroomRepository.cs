using Phoenix.DataHandle.Main.Models;
using System;

namespace Phoenix.DataHandle.Repositories
{
    public class ClassroomRepository : Repository<Classroom>
    {
        public ClassroomRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override Classroom Create(Classroom tModel)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.CreatedAt = DateTimeOffset.Now;

            return base.Create(tModel);
        }

        public override Classroom Update(Classroom tModel)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }
    }
}
