using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class LectureApi : ILecture, IModelApi
    {
        private LectureApi()
        {
            this.Exams = new List<ExamApi>();
            this.Exercises = new List<ExerciseApi>();

            this.Attendees = new List<IAspNetUser>();
        }

        [JsonConstructor]
        public LectureApi(int id, CourseApi course, ClassroomApi? classroom, ScheduleApi? schedule, DateTimeOffset startDateTime, 
            DateTimeOffset endDateTime, LectureStatus status, string? onlineMeetingLink, bool attendancesNoted,
            string? comments, LectureCreatedBy createdBy, List<ExamApi>? exams, List<ExerciseApi>? exercises)
            : this()
        {
            this.Id = id;
            this.Course = course;
            this.Classroom = classroom;
            this.Schedule = schedule;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.Status = status;
            this.OnlineMeetingLink = onlineMeetingLink;
            this.AttendancesNoted = attendancesNoted;
            this.Comments = comments;
            this.CreatedBy = createdBy;

            if (exams is not null)
                this.Exams = exams;
            if (exercises is not null)
                this.Exercises = exercises;
        }

        public LectureApi(ILecture lecture, bool include = false)
            : this(0, null!, null, null, lecture.StartDateTime, lecture.EndDateTime, lecture.Status,
                  lecture.OnlineMeetingLink, lecture.AttendancesNoted, lecture.Comments,
                  lecture.CreatedBy, null, null)
        {
            if (lecture is Lecture lecture1)
                this.Id = lecture1.Id;

            if (!include)
                return;

            if (lecture.Course is not null)
                this.Course = new CourseApi(lecture.Course);
            if (lecture.Classroom is not null)
                this.Classroom = new ClassroomApi(lecture.Classroom);
            if (lecture.Schedule is not null)
                this.Schedule = new ScheduleApi(lecture.Schedule);

            this.Exams = lecture.Exams.Select(e => new ExamApi(e)).ToList();
            this.Exercises = lecture.Exercises.Select(e => new ExerciseApi(e)).ToList();
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "course")]
        public CourseApi Course { get; } = null!;

        [JsonProperty(PropertyName = "classroom")]
        public ClassroomApi? Classroom { get; }

        [JsonProperty(PropertyName = "schedule")]
        public ScheduleApi? Schedule { get; }

        [JsonProperty(PropertyName = "start_datetime")]
        public DateTimeOffset StartDateTime { get; }

        [JsonProperty(PropertyName = "end_datetime")]
        public DateTimeOffset EndDateTime { get; }

        [JsonProperty(PropertyName = "status")]
        public LectureStatus Status { get; }

        [JsonProperty(PropertyName = "online_meeting_link")]
        public string? OnlineMeetingLink { get; }

        [JsonProperty(PropertyName = "attendances_noted")]
        public bool AttendancesNoted { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        [JsonProperty(PropertyName = "created_by")]
        public LectureCreatedBy CreatedBy { get; }

        [JsonProperty(PropertyName = "exams")]
        public List<ExamApi> Exams { get; }

        [JsonProperty(PropertyName = "exercises")]
        public List<ExerciseApi> Exercises { get; }


        ICourse ILecture.Course => this.Course;

        IClassroom? ILecture.Classroom => this.Classroom;

        ISchedule? ILecture.Schedule => this.Schedule;

        IEnumerable<IExam> ILecture.Exams => this.Exams;

        IEnumerable<IExercise> ILecture.Exercises => this.Exercises;

        [JsonIgnore]
        public IEnumerable<IAspNetUser> Attendees { get; }
    }
}
