using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class ScheduleRepository : ObviableRepository<Schedule>
    {
        public ScheduleRepository(PhoenixContext dbContext)
            : base(dbContext) 
        {
        }

        public static Expression<Func<Schedule, bool>> GetUniqueExpression(
            int courseId, DayOfWeek dayOfWeek, DateTime startTime)
        {
            return s => s.CourseId == courseId
                     && s.DayOfWeek == dayOfWeek
                     && s.StartTime.TimeOfDay == startTime.TimeOfDay;
        }

        #region Find Unique

        public Schedule? FindUnique(int courseId, DayOfWeek dayOfWeek, DateTime startTime)
        {
            return FindUnique(GetUniqueExpression(courseId, dayOfWeek, startTime));
        }

        public Schedule? FindUnique(int courseId, ISchedule schedule)
        {
            if (schedule is null)
                throw new ArgumentNullException(nameof(schedule));

            return FindUnique(courseId, schedule.DayOfWeek, schedule.StartTime);
        }

        public async Task<Schedule?> FindUniqueAsync(int courseId, DayOfWeek dayOfWeek, DateTime startTime,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(courseId, dayOfWeek, startTime),
                cancellationToken);
        }

        public async Task<Schedule?> FindUniqueAsync(int courseId, ISchedule schedule,
            CancellationToken cancellationToken = default)
        {
            if (schedule is null)
                throw new ArgumentNullException(nameof(schedule));

            return await FindUniqueAsync(courseId, schedule.DayOfWeek, schedule.StartTime,
                cancellationToken);
        }

        #endregion

        #region Search

        public IQueryable<Schedule> Search(int? courseId = null, int? classroomId = null)
        {
            var schedules = Find();

            if (courseId.HasValue)
                schedules = schedules.Where(s => s.CourseId == courseId);
            if (classroomId.HasValue)
                schedules = schedules.Where(s => s.ClassroomId == classroomId);

            return schedules;
        }

        #endregion
    }
}
