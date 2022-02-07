using System;
using Newtonsoft.Json;
using System.Globalization;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.Main.Models;
using System.Linq.Expressions;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.DataEntry.Models.Extensions;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class ScheduleACF : IModelACF
    {
        [JsonProperty(PropertyName = "course_code")]
        public short CourseCode { get; }

        [JsonProperty(PropertyName = "day")]
        public string DayName { get; }

        [JsonProperty(PropertyName = "start_time")]
        private string StartTimeString { get; }

        [JsonProperty(PropertyName = "end_time")]
        private string EndTimeString { get; }

        [JsonProperty(PropertyName = "classroom")]
        public string? ClassroomName { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        public DayOfWeek Day { get; }

        [JsonConstructor]
        public ScheduleACF(short courseCode, string dayName, string startTimeString, string endTimeString, string? classroomName, string? comments) 
        {
            if (string.IsNullOrWhiteSpace(dayName))
                throw new ArgumentNullException(nameof(dayName));
            if (string.IsNullOrWhiteSpace(startTimeString))
                throw new ArgumentNullException(nameof(startTimeString));
            if (string.IsNullOrWhiteSpace(endTimeString))
                throw new ArgumentNullException(nameof(endTimeString));

            this.CourseCode = courseCode;
            this.DayName = dayName;
            this.StartTimeString = startTimeString;
            this.EndTimeString = endTimeString;
            this.ClassroomName = string.IsNullOrWhiteSpace(classroomName) ? null : classroomName.Trim().Truncate(255).ToTitleCase();
            this.Comments = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();

            this.Day = (DayOfWeek)Array.FindIndex(CultureInfo.InvariantCulture.DateTimeFormat.DayNames,
                d => d.Equals(this.DayName, StringComparison.InvariantCultureIgnoreCase));
        }

        public Expression<Func<Schedule, bool>> GetUniqueExpression(SchoolUnique schoolUnique, string schoolTimeZone) => s =>
            s.Course.School.NormalizedName == schoolUnique.NormalizedSchoolName &&
            s.Course.School.NormalizedCity == schoolUnique.NormalizedSchoolCity &&
            s.Course.Code == this.CourseCode &&
            s.DayOfWeek == this.Day &&
            s.StartTime == GetStartTime(schoolTimeZone);

        public Expression<Func<Schedule, bool>> GetUniqueExpression(int schoolId, string schoolTimeZone) => s =>
            s.Course.School.Id == schoolId &&
            s.Course.Code == this.CourseCode &&
            s.DayOfWeek == this.Day &&
            s.StartTime == GetStartTime(schoolTimeZone);

        public DateTimeOffset GetStartTime(string schoolTimeZone) => CalendarExtensions.ParseExact(this.StartTimeString, "d/M/yyyy", schoolTimeZone);
        public DateTimeOffset GetEndTime(string schoolTimeZone) => CalendarExtensions.ParseExact(this.EndTimeString, "d/M/yyyy", schoolTimeZone);
    }
}
