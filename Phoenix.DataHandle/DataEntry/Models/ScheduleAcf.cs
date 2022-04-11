using System;
using Newtonsoft.Json;
using System.Globalization;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.Main.Models;
using System.Linq.Expressions;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using System.Collections.Generic;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class ScheduleAcf : IModelAcf, ISchedule
    {
        private ScheduleAcf()
        {
            this.Lectures = new List<ILecture>();
        }

        [JsonConstructor]
        public ScheduleAcf(short course_code, string? classroom, string day,
            string start_time, string end_time, string? comments)
            : this()
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

            this.StartTime = GetScheduleTime(start_time);
            this.EndTime = GetScheduleTime(end_time);

            if (!string.IsNullOrWhiteSpace(classroom))
            {
                this.Classroom = new Classroom
                {
                    Name = classroom.Trim().ToTitleCase(),
                    NormalizedName = classroom.ToUpper()
                };

                this.ClassroomName = this.Classroom.Name;
            }

            this.DayString = day;
            this.StartTimeString = start_time;
            this.EndTimeString = end_time;
        }

        public Expression<Func<Schedule, bool>> GetUniqueExpression(SchoolUnique schoolUnique) => s =>
            s.Course.School.Code == schoolUnique.Code &&
            s.Course.Code == this.CourseCode &&
            s.DayOfWeek == this.DayOfWeek &&
            s.StartTime == this.StartTime;

        public Expression<Func<Schedule, bool>> GetUniqueExpression(int schoolId) => s =>
            s.Course.School.Id == schoolId &&
            s.Course.Code == this.CourseCode &&
            s.DayOfWeek == this.DayOfWeek &&
            s.StartTime == this.StartTime;

        private DateTime GetScheduleTime(string timeString) =>
            DateTime.ParseExact(timeString, "H:m", CultureInfo.InvariantCulture);


        [JsonProperty(PropertyName = "course_code")]
        public short CourseCode { get; }

        [JsonProperty(PropertyName = "day")]
        public string DayString { get; } = null!;

        [JsonProperty(PropertyName = "start_time")]
        public string StartTimeString { get; } = null!;

        [JsonProperty(PropertyName = "end_time")]
        public string EndTimeString { get; } = null!;

        [JsonProperty(PropertyName = "classroom")]
        public string ClassroomName { get; } = null!;

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }
        
        [JsonIgnore]
        public IClassroom Classroom { get; } = null!;

        [JsonIgnore]
        public DayOfWeek DayOfWeek { get; }

        [JsonIgnore]
        public DateTime StartTime { get; private set; }

        [JsonIgnore]
        public DateTime EndTime { get; private set; }


        [JsonIgnore]
        public ICourse Course { get; } = null!;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; set; }
    }
}
