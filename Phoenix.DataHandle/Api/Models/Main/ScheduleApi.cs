using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

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
            DateTimeOffset startTime, DateTimeOffset endTime, string? comments)
            : this()
        {
            if (course is null)
                throw new ArgumentNullException(nameof(course));

            this.Id = id;
            this.Course = course;
            this.Classroom = classroom;
            this.DayOfWeek = dayOfWeek;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Comments = comments;
        }

        public ScheduleApi(ISchedule schedule, int id = 0)
            : this(id, new CourseApi(schedule.Course), null, schedule.DayOfWeek, schedule.StartTime, schedule.EndTime, schedule.Comments)
        {
            if (schedule.Classroom is not null)
                this.Classroom = new ClassroomApi(schedule.Classroom);
        }

        public ScheduleApi(Schedule schedule)
            : this(schedule, schedule.Id)
        {
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
        public DateTimeOffset StartTime { get; }

        [JsonProperty(PropertyName = "end_time")]
        public DateTimeOffset EndTime { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        
        ICourse ISchedule.Course => this.Course;

        IClassroom? ISchedule.Classroom => this.Classroom;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; }
    }
}
