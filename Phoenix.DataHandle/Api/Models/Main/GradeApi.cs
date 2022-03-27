using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using System;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class GradeApi : IGrade, IModelApi
    {
        [JsonConstructor]
        public GradeApi(int id, AspNetUserApi student, CourseApi? course, ExamApi? exam, ExerciseApi? exercise,
            decimal score, string? topic, string? justification)
        {
            this.Id = id;
            this.Student = student;
            this.Course = course;
            this.Exam = exam;
            this.Exercise = exercise;
            this.Score = score;
            this.Topic = topic;
            this.Justification = justification;
        }

        public GradeApi(IGrade grade, bool include = false)
            : this(0, null!, null, null, null, grade.Score, grade.Topic, grade.Justification)
        {
            if (grade is Grade grade1)
                this.Id = grade1.Id;

            if (!include)
                return;

            if (grade.Student is not null)
                this.Student = new AspNetUserApi(grade.Student);
            if (grade.Course is not null)
                this.Course = new CourseApi(grade.Course);
            if (grade.Exam is not null)
                this.Exam = new ExamApi(grade.Exam);
            if (grade.Exercise is not null)
                this.Exercise = new ExerciseApi(grade.Exercise);
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "student")]
        public AspNetUserApi Student { get; } = null!;

        [JsonProperty(PropertyName = "course")]
        public CourseApi? Course { get; }

        [JsonProperty(PropertyName = "exam")]
        public ExamApi? Exam { get; }

        [JsonProperty(PropertyName = "exercise")]
        public ExerciseApi? Exercise { get; }

        [JsonProperty(PropertyName = "score")]
        public decimal Score { get; }

        [JsonProperty(PropertyName = "topic")]
        public string? Topic { get; }

        [JsonProperty(PropertyName = "justification")]
        public string? Justification { get; }


        IAspNetUser IGrade.Student => this.Student;

        ICourse? IGrade.Course => this.Course;

        IExam? IGrade.Exam => this.Exam;

        IExercise? IGrade.Exercise => this.Exercise;
    }
}
