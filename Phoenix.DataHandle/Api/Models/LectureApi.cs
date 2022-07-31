using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Api.Models
{
    public class LectureApi : ILectureApi, IModelApi
    {
        [JsonConstructor]
        public LectureApi(int id, int courseId, int? classroomId, int? scheduleId,
            DateTimeOffset startDateTime, DateTimeOffset endDateTime, string? onlineMeetingLink,
            bool attendancesNoted, string? comments)
        {
            this.Id = id;
            this.CourseId = courseId;
            this.ClassroomId = classroomId;
            this.ScheduleId = scheduleId;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.OnlineMeetingLink = onlineMeetingLink;
            this.AttendancesNoted = attendancesNoted;
            this.Comments = comments;
        }

        public LectureApi(int id, int courseId, int? classroomId, int? scheduleId, ILectureBase lecture)
            : this(id, courseId, classroomId, scheduleId,
                  lecture.StartDateTime, lecture.EndDateTime, lecture.OnlineMeetingLink,
                  lecture.AttendancesNoted, lecture.Comments)
        {
            this.Occasion = lecture.Occasion;
            this.IsCancelled = lecture.IsCancelled;
        }

        public LectureApi(Lecture lecture)
            : this(lecture.Id, lecture.CourseId, lecture.ClassroomId, lecture.ScheduleId, lecture)
        {
            this.ReplacementLectureId = lecture.ReplacementLectureId;
        }

        public Lecture ToLecture()
        {
            return new Lecture()
            {
                Id = this.Id,
                CourseId = this.CourseId,
                ClassroomId = this.ClassroomId,
                ScheduleId = this.ScheduleId,
                StartDateTime = this.StartDateTime,
                EndDateTime = this.EndDateTime,
                OnlineMeetingLink = this.OnlineMeetingLink,
                AttendancesNoted = this.AttendancesNoted,
                Comments = this.Comments
            };
        }

        public Lecture ToLecture(Lecture lectureToUpdate)
        {
            lectureToUpdate.CourseId = this.CourseId;
            lectureToUpdate.ClassroomId = this.ClassroomId;
            lectureToUpdate.ScheduleId = this.ScheduleId;
            lectureToUpdate.StartDateTime = this.StartDateTime;
            lectureToUpdate.EndDateTime = this.EndDateTime;
            lectureToUpdate.OnlineMeetingLink = this.OnlineMeetingLink;
            lectureToUpdate.AttendancesNoted = this.AttendancesNoted;
            lectureToUpdate.Comments = this.Comments;

            return lectureToUpdate;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("course_id", Required = Required.Always)]
        public int CourseId { get; }

        [JsonProperty("classroom_id")]
        public int? ClassroomId { get; }

        [JsonProperty("schedule_id")]
        public int? ScheduleId { get; }

        [JsonProperty("start_datetime", Required = Required.Always)]
        public DateTimeOffset StartDateTime { get; }

        [JsonProperty("end_datetime", Required = Required.Always)]
        public DateTimeOffset EndDateTime { get; }

        [JsonProperty("online_meeting_link")]
        public string? OnlineMeetingLink { get; }

        [JsonProperty("occasion")]
        public LectureOccasion Occasion { get; }

        [JsonProperty("attendances_noted", Required = Required.Always)]
        public bool AttendancesNoted { get; }

        [JsonProperty("is_cancelled")]
        public bool IsCancelled { get; }

        [JsonProperty("replacement_lecture_id")]
        public int? ReplacementLectureId { get; }

        [JsonProperty("comments")]
        public string? Comments { get; }
    }
}
