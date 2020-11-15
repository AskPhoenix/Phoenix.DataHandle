using System;
using Newtonsoft.Json;
using System.Globalization;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.Main.Models;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.WordPress.Models
{
    public class ScheduleACF : IModelACF<Schedule>
    {
        [JsonProperty(PropertyName = "code")]
        public short Code { get; set; }

        [JsonProperty(PropertyName = "course_code")]
        public short CourseCode { get; set; }

        [JsonProperty(PropertyName = "classroom")]
        public string ClassroomName { get => classroomName; set => classroomName = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string classroomName;

        [JsonProperty(PropertyName = "day")]
        public string DayName { get; set; }

        [JsonProperty(PropertyName = "start_time")]
        private string StartTimeString { get; set; }
        public DateTimeOffset StartTime { get => CalendarExtensions.GetDateTimeOffsetFromString(StartTimeString, "H:mm:ss"); }

        [JsonProperty(PropertyName = "end_time")]
        private string EndTimeString { get; set; }
        public DateTimeOffset EndTime { get => CalendarExtensions.GetDateTimeOffsetFromString(EndTimeString, "H:mm:ss"); }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get => comments; set => comments = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string comments;

        public int CourseId { get; set; }
        public int? ClassroomId { get; set; }

        private DayOfWeek GetDayOfWeek(string dayName)
        {
            return (DayOfWeek)Array.FindIndex(CultureInfo.CurrentCulture.DateTimeFormat.DayNames, s => s == dayName);
        }

        public Expression<Func<Schedule, bool>> MatchesUnique => s => s != null && s.CourseId == this.CourseId && s.Code == this.Code;

        public Schedule ToContext()
        {
            return new Schedule()
            {
                CourseId = this.CourseId,
                Code = this.Code,
                ClassroomId = this.ClassroomId,
                DayOfWeek = GetDayOfWeek(this.DayName),
                StartTime = this.StartTime,
                EndTime = this.EndTime,
                Info = this.Comments,
                CreatedAt = DateTimeOffset.Now
            };
        }

        public IModelACF<Schedule> WithTitleCase()
        {
            return new ScheduleACF()
            {
                Code = this.Code,
                CourseCode = this.CourseCode,
                ClassroomName = this.ClassroomName?.UpperToTitleCase(),
                DayName = this.DayName,
                StartTimeString = this.StartTimeString,
                EndTimeString = this.EndTimeString,
                Comments = this.Comments,
                CourseId = this.CourseId
            };
        }
    }
}
