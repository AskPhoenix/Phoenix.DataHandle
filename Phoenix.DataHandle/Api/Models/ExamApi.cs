using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
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

        public Exam ToExam()
        {
            return new Exam()
            {
                Id = this.Id,
                LectureId = this.LectureId,
                Name = this.Name,
                Comments = this.Comments
            };
        }

        public Exam ToExam(Exam examToUpdate)
        {
            examToUpdate.LectureId = this.LectureId;
            examToUpdate.Name = this.Name;
            examToUpdate.Comments = this.Comments;

            return examToUpdate;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("lecture_id", Required = Required.Always)]
        public int LectureId { get; }

        [JsonProperty("name")]
        public string? Name { get; }

        [JsonProperty("comments")]
        public string? Comments { get; }
    }
}
