using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class ScheduleApi : IScheduleApi, IModelApi
    {
        [JsonConstructor]
        public ScheduleApi(int courseId, int? classroomId, DayOfWeek dayOfWeek,
            DateTime startTime, DateTime endTime, string? comments)
        {
            this.Id = 0;
            this.CourseId = courseId;
            this.ClassroomId = classroomId;
            this.DayOfWeek = dayOfWeek;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Comments = comments;
        }

        public ScheduleApi(int courseId, int? classroomId, IScheduleBase schedule)
            : this(courseId, classroomId, schedule.DayOfWeek,
                  schedule.StartTime, schedule.EndTime, schedule.Comments)
        {
        }

        public ScheduleApi(Schedule schedule)
            : this(schedule.CourseId, schedule.ClassroomId, schedule)
        {
            this.Id = schedule.Id;
        }

        public Schedule ToSchedule()
        {
            return new Schedule()
            {
                Id = this.Id,
                CourseId = this.CourseId,
                ClassroomId = this.ClassroomId,
                DayOfWeek = this.DayOfWeek,
                StartTime = this.StartTime,
                EndTime = this.EndTime,
                Comments = this.Comments
            };
        }

        public Schedule ToSchedule(Schedule scheduleToUpdate)
        {
            scheduleToUpdate.CourseId = this.CourseId;
            scheduleToUpdate.ClassroomId = this.ClassroomId;
            scheduleToUpdate.DayOfWeek = this.DayOfWeek;
            scheduleToUpdate.StartTime = this.StartTime;
            scheduleToUpdate.EndTime = this.EndTime;
            scheduleToUpdate.Comments = this.Comments;

            return scheduleToUpdate;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("course_id", Required = Required.Always)]
        public int CourseId { get; }

        [JsonProperty("classroom_id")]
        public int? ClassroomId { get; }

        [JsonProperty("day_of_week", Required = Required.Always)]
        public DayOfWeek DayOfWeek { get; }

        [JsonProperty("start_time", Required = Required.Always)]
        public DateTime StartTime { get; }

        [JsonProperty("end_time", Required = Required.Always)]
        public DateTime EndTime { get; }

        [JsonProperty("comments")]
        public string? Comments { get; }
    }
}
