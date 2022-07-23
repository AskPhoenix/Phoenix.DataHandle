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
        public ScheduleApi(int id, int courseId, int? classroomId, DayOfWeek dayOfWeek,
            DateTime startTime, DateTime endTime, string? comments)
        {
            this.Id = id;
            this.CourseId = courseId;
            this.ClassroomId = classroomId;
            this.DayOfWeek = dayOfWeek;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Comments = comments;
        }

        public ScheduleApi(int id, int courseId, int? classroomId, IScheduleBase schedule)
            : this(id, courseId, classroomId, schedule.DayOfWeek,
                  schedule.StartTime, schedule.EndTime, schedule.Comments)
        {
        }

        public ScheduleApi(Schedule schedule)
            : this(schedule.Id, schedule.CourseId, schedule.ClassroomId, schedule)
        {
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

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "course_id")]
        public int CourseId { get; }

        [JsonProperty(PropertyName = "classroom_id")]
        public int? ClassroomId { get; }

        [JsonProperty(PropertyName = "day_of_week")]
        public DayOfWeek DayOfWeek { get; }

        [JsonProperty(PropertyName = "start_time")]
        public DateTime StartTime { get; }

        [JsonProperty(PropertyName = "end_time")]
        public DateTime EndTime { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }
    }
}
