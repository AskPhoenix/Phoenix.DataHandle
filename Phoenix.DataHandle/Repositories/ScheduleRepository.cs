using Phoenix.DataHandle.Main.Models;
using System;

namespace Phoenix.DataHandle.Repositories
{
    public class ScheduleRepository : ObviableRepository<Schedule>
    {
        public ScheduleRepository(PhoenixContext dbContext) : base(dbContext) { }

        public Schedule Update(Schedule tModel, Schedule tModelFrom)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (tModelFrom == null)
                throw new ArgumentNullException(nameof(tModelFrom));

            //The columns of the unique keys should not be copied

            tModel.ClassroomId = tModelFrom.ClassroomId;
            tModel.EndTime = tModel.EndTime.Date + tModelFrom.EndTime.TimeOfDay;
            tModel.Info = tModelFrom.Info;

            return this.Update(tModel);
        }
    }
}
