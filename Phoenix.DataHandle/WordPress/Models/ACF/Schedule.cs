using System;
using Newtonsoft.Json;
using System.Globalization;
using Phoenix.DataHandle.Utilities;

namespace Phoenix.DataHandle.WordPress.ACF
{
    class Schedule
    {
        [JsonProperty(PropertyName = "code")]
        public short Code { get; set; }

        [JsonProperty(PropertyName = "course_code")]
        public short CourseCode { get; set; }

        [JsonProperty(PropertyName = "classroom")]
        public string Classroom { get => classroom; set => classroom = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string classroom;

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

        private DayOfWeek GetDayOfWeek(string dayName)
        {
            return (DayOfWeek)Array.FindIndex(CultureInfo.CurrentCulture.DateTimeFormat.DayNames, s => s == dayName);
        }

        public bool MatchesUnique(Main.Models.Schedule ctxSchedule)
        {
            return ctxSchedule != null
                && ctxSchedule.CourseId == this.CourseId
                && ctxSchedule.Code == this.Code;
        }

        public Main.Models.Schedule ToContextSchedule(int courseId, int? classroomId)
        {
            return new Main.Models.Schedule()
            {
                CourseId = courseId,
                Code = this.Code,
                ClassroomId = classroomId,
                DayOfWeek = GetDayOfWeek(this.DayName),
                StartTime = this.StartTime,
                EndTime = this.EndTime,
                Info = this.Comments,
                CreatedAt = DateTimeOffset.Now
            };
        }

        public Schedule WithTitleCaseText()
        {
            return new Schedule()
            {
                Code = this.Code,
                CourseCode = this.CourseCode,
                Classroom = this.Classroom?.UpperToTitleCase(),
                DayName = this.DayName,
                StartTimeString = this.StartTimeString,
                EndTimeString = this.EndTimeString,
                Comments = this.Comments,
                CourseId = this.CourseId
            };
        }
    }
}
