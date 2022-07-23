using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class GradeApi : IGradeApi, IModelApi
    {
        [JsonConstructor]
        public GradeApi(int id, int studentId, int? courseId, int? examId, int? exerciseId,
            decimal score, string? topic, string? justification)
        {
            this.Id = id;
            this.StudentId = studentId;
            this.CourseId = courseId;
            this.ExamId = examId;
            this.ExerciseId = exerciseId;
            this.Score = score;
            this.Topic = topic;
            this.Justification = justification;
        }

        public GradeApi(int id, int studentId, int? courseId, int? examId, int? exerciseId, IGradeBase grade)
            : this(id, studentId, courseId, examId, exerciseId, grade.Score, grade.Topic, grade.Justification)
        {
        }

        public GradeApi(Grade grade)
            : this(grade.Id, grade.StudentId, grade.CourseId, grade.ExamId, grade.ExerciseId, grade)
        {
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "student_id")]
        public int StudentId { get; }

        [JsonProperty(PropertyName = "course_id")]
        public int? CourseId { get; }

        [JsonProperty(PropertyName = "exam_id")]
        public int? ExamId { get; }

        [JsonProperty(PropertyName = "exercise_id")]
        public int? ExerciseId { get; }

        [JsonProperty(PropertyName = "score")]
        public decimal Score { get; }

        [JsonProperty(PropertyName = "topic")]
        public string? Topic { get; }

        [JsonProperty(PropertyName = "justification")]
        public string? Justification { get; }
    }
}
