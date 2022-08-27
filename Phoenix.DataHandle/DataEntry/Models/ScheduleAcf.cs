using Newtonsoft.Json;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.DataEntry.Entities;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using System.Globalization;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class ScheduleAcf : IModelAcf, IScheduleAcf
    {
        private const string TimeFormat = "H:m";

        [JsonConstructor]
        public ScheduleAcf(short course_code, string? classroom, string day,
            string start_time, string end_time, string? comments)
        {
            if (string.IsNullOrWhiteSpace(day))
                throw new ArgumentNullException(nameof(day));
            if (string.IsNullOrWhiteSpace(start_time))
                throw new ArgumentNullException(nameof(start_time));
            if (string.IsNullOrWhiteSpace(end_time))
                throw new ArgumentNullException(nameof(end_time));

            this.CourseCode = course_code;
            this.Comments = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();

            this.DayOfWeek = (DayOfWeek)Array.FindIndex(
                CultureInfo.InvariantCulture.DateTimeFormat.DayNames, 
                d => d.Equals(day, StringComparison.InvariantCultureIgnoreCase));

            this.StartTime = CalendarExtensions.ParseExact(start_time, TimeFormat);
            this.EndTime = CalendarExtensions.ParseExact(end_time, TimeFormat);
    
            this.ClassroomName = classroom?.Trim().ToTitleCase();
            if (!string.IsNullOrWhiteSpace(ClassroomName))
                this.Classroom = new Classroom() { Name = this.ClassroomName }.Normalize();
            
            this.DayString = day;
            this.StartTimeString = start_time;
            this.EndTimeString = end_time;
        }

        public Schedule ToSchedule(int courseId, int? classroomId)
        {
            return new()
            {
                CourseId = courseId,
                DayOfWeek = this.DayOfWeek,
                StartTime = this.StartTime,
                EndTime = this.EndTime,
                Comments = this.Comments,

                ClassroomId = classroomId
            };
        }

        public Schedule ToSchedule(Schedule scheduleToUpdate, int courseId, int? classroomId)
        {
            if (scheduleToUpdate is null)
                throw new ArgumentNullException(nameof(scheduleToUpdate));

            scheduleToUpdate.CourseId = courseId;
            scheduleToUpdate.DayOfWeek = this.DayOfWeek;
            scheduleToUpdate.StartTime = this.StartTime;
            scheduleToUpdate.EndTime = this.EndTime;
            scheduleToUpdate.Comments = this.Comments;

            scheduleToUpdate.ClassroomId = classroomId;

            return scheduleToUpdate;
        }

        [JsonProperty(PropertyName = "course_code")]
        public short CourseCode { get; }

        [JsonProperty(PropertyName = "day")]
        public string DayString { get; } = null!;

        [JsonProperty(PropertyName = "start_time")]
        public string StartTimeString { get; } = null!;

        [JsonProperty(PropertyName = "end_time")]
        public string EndTimeString { get; } = null!;

        [JsonProperty(PropertyName = "classroom")]
        public string? ClassroomName { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        
        [JsonIgnore]
        public DayOfWeek DayOfWeek { get; }

        [JsonIgnore]
        public DateTime StartTime { get; private set; }

        [JsonIgnore]
        public DateTime EndTime { get; private set; }


        [JsonIgnore]
        public Classroom Classroom { get; } = null!;

        IClassroomBase IScheduleAcf.Classroom => this.Classroom;
    }
}
