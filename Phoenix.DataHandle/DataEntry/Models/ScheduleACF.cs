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
    public class ScheduleACF : IModelACF, ISchedule
    {
        private ScheduleACF()
        {
            this.Lectures = new List<ILecture>();
        }

        [JsonConstructor]
        public ScheduleACF(short courseCode, string? classroomName, string dayName, string startTimeString, string endTimeString, string? comments)
            : this()
        {
            if (string.IsNullOrWhiteSpace(dayName))
                throw new ArgumentNullException(nameof(dayName));
            if (string.IsNullOrWhiteSpace(startTimeString))
                throw new ArgumentNullException(nameof(startTimeString));
            if (string.IsNullOrWhiteSpace(endTimeString))
                throw new ArgumentNullException(nameof(endTimeString));

            this.CourseCode = courseCode;
            this.ClassroomName = string.IsNullOrWhiteSpace(classroomName) ? null : classroomName.Trim().Truncate(255).ToTitleCase();
            this.DayName = dayName;
            this.StartTimeString = startTimeString;
            this.EndTimeString = endTimeString;
            this.Comments = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();

            this.DayOfWeek = (DayOfWeek)Array.FindIndex(
                CultureInfo.InvariantCulture.DateTimeFormat.DayNames, 
                d => d.Equals(this.DayName, StringComparison.InvariantCultureIgnoreCase));

            this.StartTime = GetScheduleTime(selStartTime: true);
            this.EndTime = GetScheduleTime(selStartTime: false);
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

        private DateTimeOffset GetScheduleTime(bool selStartTime)
        {
            var timeString = selStartTime ? this.StartTimeString : this.EndTimeString;
            return CalendarExtensions.ParseTime(timeString, this.TimeZone);
        }


        [JsonProperty(PropertyName = "course_code")]
        public short CourseCode { get; }

        [JsonProperty(PropertyName = "classroom")]
        // TODO: Create IClassroom object too
        public string? ClassroomName { get; }

        [JsonProperty(PropertyName = "day")]
        public string DayName { get; } = null!;

        [JsonIgnore]
        public DayOfWeek DayOfWeek { get; }

        [JsonProperty(PropertyName = "start_time")]
        public string StartTimeString { get; } = null!;

        [JsonIgnore]
        public DateTimeOffset StartTime { get; private set; }

        [JsonProperty(PropertyName = "end_time")]
        private string EndTimeString { get; } = null!;

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

                this.StartTime = GetScheduleTime(selStartTime: true);
                this.EndTime = GetScheduleTime(selStartTime: false);
            }
        }
        private string timezone = "UTC";


        [JsonIgnore]
        public ICourse Course { get; } = null!;

        [JsonIgnore]
        public IClassroom Classroom { get; } = null!;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; set; }
    }
}
