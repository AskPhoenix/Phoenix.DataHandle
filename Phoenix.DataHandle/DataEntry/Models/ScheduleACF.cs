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
        public ScheduleAcf(short courseCode, string? classroomName, string dayName, string startTimeString, string endTimeString, string? comments)
            : this()
        {
            if (string.IsNullOrWhiteSpace(dayName))
                throw new ArgumentNullException(nameof(dayName));
            if (string.IsNullOrWhiteSpace(startTimeString))
                throw new ArgumentNullException(nameof(startTimeString));
            if (string.IsNullOrWhiteSpace(endTimeString))
                throw new ArgumentNullException(nameof(endTimeString));

            this.CourseCode = courseCode;
            this.Comments = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();

            this.DayOfWeek = (DayOfWeek)Array.FindIndex(
                CultureInfo.InvariantCulture.DateTimeFormat.DayNames, 
                d => d.Equals(dayName, StringComparison.InvariantCultureIgnoreCase));

            this.StartTime = CalendarExtensions.ParseTime(startTimeString, this.TimeZone);
            this.EndTime = CalendarExtensions.ParseTime(endTimeString, this.TimeZone);

            if (!string.IsNullOrWhiteSpace(classroomName))
            {
                this.Classroom = new Classroom
                {
                    Name = classroomName.Trim().Truncate(255).ToTitleCase(),
                    NormalizedName = classroomName.ToUpper()
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
        public string TimeZone
        {
            get => timezone;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(TimeZone));

                timezone = value;

                this.StartTime = this.StartTime.SetOffsetFromTimeZone(timezone);
                this.EndTime = this.EndTime.SetOffsetFromTimeZone(timezone);
            }
        }
        private string timezone = "UTC";


        [JsonIgnore]
        public ICourse Course { get; } = null!;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; set; }
    }
}
