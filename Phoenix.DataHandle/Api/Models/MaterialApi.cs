using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class MaterialApi : IMaterialApi, IModelApi
    {
        [JsonConstructor]
        public MaterialApi(int id, int examId, int? bookId, string? chapter, string? section, string? comments)
        {
            this.Id = id;
            this.ExamId = examId;
            this.BookId = bookId;
            this.Chapter = chapter;
            this.Section = section;
            this.Comments = comments;
        }

        public MaterialApi(int id, int examId, int? bookId, IMaterialBase material)
            : this(id, examId, bookId, material.Chapter, material.Section, material.Comments)
        {
        }

        public MaterialApi(Material material)
            : this(material.Id, material.ExamId, material.BookId, material)
        {
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "exam_id")]
        public int ExamId { get; }

        [JsonProperty(PropertyName = "book_id")]
        public int? BookId { get; }

        [JsonProperty(PropertyName = "chapter")]
        public string? Chapter { get; }

        [JsonProperty(PropertyName = "section")]
        public string? Section { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }
    }
}
