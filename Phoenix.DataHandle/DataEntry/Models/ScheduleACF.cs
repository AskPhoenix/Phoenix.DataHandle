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
                    Name = classroom.Trim().Truncate(255).ToTitleCase(),
                    NormalizedName = classroom.ToUpper()
                };
            }
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

        private DateTimeOffset GetScheduleTime(string timeString) =>
            DateTime.ParseExact(timeString, "H:m", CultureInfo.InvariantCulture);


        [JsonProperty(PropertyName = "course_code")]
        public short CourseCode { get; }

        [JsonIgnore]
        public IClassroom Classroom { get; } = null!;

        [JsonIgnore]
        public DayOfWeek DayOfWeek { get; }

        [JsonIgnore]
        public DateTimeOffset StartTime { get; private set; }

        [JsonIgnore]
        public DateTimeOffset EndTime { get; private set; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }


        [JsonIgnore]
        public ICourse Course { get; } = null!;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; set; }
    }
}
