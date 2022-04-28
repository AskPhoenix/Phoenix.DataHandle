using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class ScheduleApi : ISchedule, IModelApi
    {
        private ScheduleApi()
        {
            this.Lectures = new List<ILecture>();
        }

        [JsonConstructor]
        public ScheduleApi(int id, CourseApi course, ClassroomApi? classroom, DayOfWeek dayOfWeek, 
            DateTime startTime, DateTime endTime, string? comments)
            : this()
        {
            this.Id = id;
            this.Course = course;
            this.Classroom = classroom;
            this.DayOfWeek = dayOfWeek;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Comments = comments;
        }

        public ScheduleApi(ISchedule schedule, bool include = false)
            : this(0, null!, null, schedule.DayOfWeek, schedule.StartTime, schedule.EndTime, schedule.Comments)
        {
            if (schedule is Schedule schedule1)
                this.Id = schedule1.Id;

            if (!include)
                return;

            if (schedule.Comments is not null)
                this.Course = new CourseApi(schedule.Course);
            if (schedule.Classroom is not null)
                this.Classroom = new ClassroomApi(schedule.Classroom);
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "course")]
        public CourseApi Course { get; } = null!;

        [JsonProperty(PropertyName = "classroom")]
        public ClassroomApi? Classroom { get; }

        [JsonProperty(PropertyName = "day_of_week")]
        public DayOfWeek DayOfWeek { get; }

        [JsonProperty(PropertyName = "start_time")]
        public DateTime StartTime { get; }

        [JsonProperty(PropertyName = "end_time")]
        public DateTime EndTime { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        
        ICourse ISchedule.Course => this.Course;

        IClassroom? ISchedule.Classroom => this.Classroom;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; }
    }
}
