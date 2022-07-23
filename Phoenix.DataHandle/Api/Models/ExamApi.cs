using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class ExamApi : IExamApi, IModelApi
    {
        [JsonConstructor]
        public ExamApi(int id, int lectureId, string? name, string? comments)
        {
            this.Id = id;
            this.LectureId = lectureId;
            this.Name = name;
            this.Comments = comments;
        }

        public ExamApi(int id, int lectureId, IExamBase exam)
            : this(id, lectureId, exam.Name, exam.Comments)
        {
        }

        public ExamApi(Exam exam)
            : this(exam.Id, exam.LectureId, exam)
        {
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "lecture_id")]
        public int LectureId { get; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }
    }
}
