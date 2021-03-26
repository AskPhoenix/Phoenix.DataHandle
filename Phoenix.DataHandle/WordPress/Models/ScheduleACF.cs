using System;
using Newtonsoft.Json;
using System.Globalization;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.Main.Models;
using System.Linq.Expressions;
using Phoenix.DataHandle.WordPress.Models.Uniques;

namespace Phoenix.DataHandle.WordPress.Models
{
    public class ScheduleACF : IModelACF<Schedule>
    {
        [JsonProperty(PropertyName = "course_code")]
        public short CourseCode { get; set; }

        [JsonProperty(PropertyName = "classroom")]
        public string ClassroomName { get => classroomName; set => classroomName = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string classroomName;

        [JsonProperty(PropertyName = "day")]
        public string DayName { get; set; }

        [JsonProperty(PropertyName = "start_time")]
        private string StartTimeString { get; set; }
        public DateTimeOffset StartTime { get => CalendarExtensions.GetDateTimeOffsetFromString(StartTimeString, "t"); }    //HH:mm

        [JsonProperty(PropertyName = "end_time")]
        private string EndTimeString { get; set; }
        public DateTimeOffset EndTime { get => CalendarExtensions.GetDateTimeOffsetFromString(EndTimeString, "t"); }        //HH:mm

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get => comments; set => comments = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string comments;

        //TODO: Get locale dynamically
        private DayOfWeek GetDayOfWeek(string dayName, string locale = "el")
        {
            return (DayOfWeek)Array.FindIndex(CultureInfo.GetCultureInfo(locale).DateTimeFormat.DayNames, s => s == dayName);
        }

        public Expression<Func<Schedule, bool>> MatchesUnique => s =>
            s.Course.School.NormalizedName == this.SchoolUnique.NormalizedSchoolName &&
            s.Course.School.NormalizedCity == this.SchoolUnique.NormalizedSchoolCity &&
            s.Course.Code == this.CourseCode &&
            s.DayOfWeek.ToString().ToUpperInvariant() == this.DayName.ToUpperInvariant() &&
            s.StartTime.ToString("t") == this.StartTimeString;

        public SchoolUnique SchoolUnique { get; set; }

        [JsonConstructor]
        public ScheduleACF(string dayName, string startTimeString) 
        {
            this.DayName = dayName;
            this.StartTimeString = startTimeString;
        }

        public ScheduleACF(string dayName, string startTimeString, CourseUnique courseUnique)
            : this(dayName, startTimeString)
        {
            this.SchoolUnique = courseUnique.SchoolUnique;
            this.CourseCode = courseUnique.Code;
        }

        public ScheduleACF(ScheduleACF other)
        {
            this.CourseCode = other.CourseCode;
            this.ClassroomName = other.ClassroomName;
            this.DayName = other.DayName;
            this.StartTimeString = other.StartTimeString;
            this.EndTimeString = other.EndTimeString;
            this.Comments = other.Comments;
        }

        public Schedule ToContext()
        {
            return new Schedule()
            {
                DayOfWeek = GetDayOfWeek(this.DayName),
                StartTime = this.StartTime,
                EndTime = this.EndTime,
                Info = this.Comments
            };
        }

        public IModelACF<Schedule> WithTitleCase()
        {
            return new ScheduleACF(this)
            {
                ClassroomName = this.ClassroomName.ToTitleCase()
            };
        }
    }
}
