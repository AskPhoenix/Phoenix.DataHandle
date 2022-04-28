﻿using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class LectureApi : ILecture, IModelApi
    {
        private LectureApi()
        {
            this.Exams = new List<ExamApi>();
            this.Exercises = new List<ExerciseApi>();
            
            this.InverseReplacementLecture = new List<ILecture>();
            this.Attendees = new List<IUser>();
        }

        [JsonConstructor]
        public LectureApi(int id, CourseApi course, ClassroomApi? classroom, ScheduleApi? schedule,
            DateTimeOffset startDateTime, DateTimeOffset endDateTime, string? onlineMeetingLink,
            bool attendancesNoted, string? comments, List<ExamApi>? exams, List<ExerciseApi>? exercises)
            : this()
        {
            this.Id = id;
            this.Course = course;
            this.Classroom = classroom;
            this.Schedule = schedule;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.OnlineMeetingLink = onlineMeetingLink;
            this.AttendancesNoted = attendancesNoted;
            this.Comments = comments;

            if (exams is not null)
                this.Exams = exams;
            if (exercises is not null)
                this.Exercises = exercises;
        }

        public LectureApi(ILecture lecture, bool include = false)
            : this(0, null!, null, null,
                  lecture.StartDateTime, lecture.EndDateTime, lecture.OnlineMeetingLink,
                  lecture.AttendancesNoted, lecture.Comments, null, null)
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

        [JsonProperty(PropertyName = "online_meeting_link")]
        public string? OnlineMeetingLink { get; }

        [JsonProperty(PropertyName = "occasion")]
        public LectureOccasion Occasion { get; }

        [JsonProperty(PropertyName = "attendances_noted")]
        public bool AttendancesNoted { get; }

        [JsonProperty(PropertyName = "is_cancelled")]
        public bool IsCancelled { get; }

        [JsonProperty(PropertyName = "replacement_lecture")]
        public LectureApi? ReplacementLecture { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        [JsonProperty(PropertyName = "exams")]
        public List<ExamApi> Exams { get; }

        [JsonProperty(PropertyName = "exercises")]
        public List<ExerciseApi> Exercises { get; }


        ICourse ILecture.Course => this.Course;

        ILecture? ILecture.ReplacementLecture => this.ReplacementLecture;

        IClassroom? ILecture.Classroom => this.Classroom;

        ISchedule? ILecture.Schedule => this.Schedule;

        IEnumerable<IExam> ILecture.Exams => this.Exams;

        IEnumerable<IExercise> ILecture.Exercises => this.Exercises;

        [JsonIgnore]
        public IEnumerable<ILecture> InverseReplacementLecture { get; }

        [JsonIgnore]
        public IEnumerable<IUser> Attendees { get; }
    }
}
