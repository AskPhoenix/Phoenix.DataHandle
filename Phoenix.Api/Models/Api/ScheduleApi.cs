using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.Api.Models.Api
{
    public class ScheduleApi : ISchedule, IModelApi
    {
        public int id { get; set; }
        public DayOfWeek dayOfWeek { get; set; }
        public DateTime startAt { get; set; }
        public DateTime endAt { get; set; }
        public ICourse Course { get; set; }
        public IClassroom Classroom { get; set; }
    }
}
