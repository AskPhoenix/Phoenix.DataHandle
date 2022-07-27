﻿using Newtonsoft.Json;
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
        }

        public LectureApi(Lecture lecture)
            : this(lecture.Id, lecture.CourseId, lecture.ClassroomId, lecture.ScheduleId, lecture)
        {
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
                Occasion = this.Occasion,
                AttendancesNoted = this.AttendancesNoted,
                IsCancelled = this.IsCancelled,
                ReplacementLectureId = this.ReplacementLectureId,
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
            lectureToUpdate.Occasion = this.Occasion;
            lectureToUpdate.AttendancesNoted = this.AttendancesNoted;
            lectureToUpdate.IsCancelled = this.IsCancelled;
            lectureToUpdate.ReplacementLectureId = this.ReplacementLectureId;
            lectureToUpdate.Comments = this.Comments;

            return lectureToUpdate;
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "course_id")]
        public int CourseId { get; }

        [JsonProperty(PropertyName = "classroom_id")]
        public int? ClassroomId { get; }

        [JsonProperty(PropertyName = "schedule_id")]
        public int? ScheduleId { get; }

        [JsonProperty(PropertyName = "start_datetime")]
        public DateTimeOffset StartDateTime { get; }

        [JsonProperty(PropertyName = "end_datetime")]
        public DateTimeOffset EndDateTime { get; }

        [JsonProperty(PropertyName = "online_meeting_link")]
        public string? OnlineMeetingLink { get; }

        [JsonProperty(PropertyName = "occasion")]
        public LectureOccasion Occasion { get; }

        [JsonProperty(PropertyName = "attendances_noted")]
        public bool AttendancesNoted { get; }

        [JsonProperty(PropertyName = "is_cancelled")]
        public bool IsCancelled { get; }

        [JsonProperty(PropertyName = "replacement_lecture_id")]
        public int? ReplacementLectureId { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }
    }
}