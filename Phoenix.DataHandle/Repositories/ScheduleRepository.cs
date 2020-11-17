using Phoenix.DataHandle.Main.Models;
using System;

namespace Phoenix.DataHandle.Repositories
{
    public class ScheduleRepository : Repository<Schedule>
    {
        public ScheduleRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override Schedule Create(Schedule tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;

            return base.Create(tModel);
        }

        public override Schedule Update(Schedule tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }

        public Schedule Update(Schedule tModel, Schedule tModelFrom)
        {
            tModel.ClassroomId = tModelFrom.ClassroomId;
            tModel.DayOfWeek = tModelFrom.DayOfWeek;
            tModel.StartTime = tModel.StartTime.Date + tModelFrom.StartTime.TimeOfDay;
            tModel.EndTime = tModel.EndTime.Date + tModelFrom.EndTime.TimeOfDay;
            tModel.Info = tModelFrom.Info;

            return this.Update(tModel);
        }
    }
}
