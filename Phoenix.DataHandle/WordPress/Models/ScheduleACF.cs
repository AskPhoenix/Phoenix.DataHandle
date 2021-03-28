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
        public string DayName { get; }

        [JsonProperty(PropertyName = "start_time")]
        private string StartTimeString { get; }
        public DateTimeOffset StartTime { get => CalendarExtensions.ParseExact(this.StartTimeString, "H:m", this.SchoolTimeZone); }

        [JsonProperty(PropertyName = "end_time")]
        private string EndTimeString { get; set; }
        public DateTimeOffset EndTime { get => CalendarExtensions.ParseExact(this.EndTimeString, "H:m", this.SchoolTimeZone); }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get => comments; set => comments = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string comments;

        public string SchoolTimeZone { get; set; }

        public DayOfWeek InvariantDayOfWeek => (DayOfWeek)Array.FindIndex(CultureInfo.InvariantCulture.DateTimeFormat.DayNames, 
            d => string.Compare(d, this.DayName, StringComparison.InvariantCultureIgnoreCase) == 0);

        public Expression<Func<Schedule, bool>> MatchesUnique => s =>
            s.Course.School.NormalizedName == this.SchoolUnique.NormalizedSchoolName &&
            s.Course.School.NormalizedCity == this.SchoolUnique.NormalizedSchoolCity &&
            s.Course.Code == this.CourseCode &&
            s.DayOfWeek == this.InvariantDayOfWeek &&
            s.StartTime == this.StartTime;

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
                DayOfWeek = this.InvariantDayOfWeek,
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
