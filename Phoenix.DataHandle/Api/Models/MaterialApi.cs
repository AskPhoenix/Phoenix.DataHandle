using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
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

        public Material ToMaterial()
        {
            return new Material()
            {
                Id = this.Id,
                ExamId = this.ExamId,
                BookId = this.BookId,
                Chapter = this.Chapter,
                Section = this.Section,
                Comments = this.Comments
            };
        }

        public Material ToMaterial(Material materialToUpdate)
        {
            materialToUpdate.ExamId = this.ExamId;
            materialToUpdate.BookId = this.BookId;
            materialToUpdate.Chapter = this.Chapter;
            materialToUpdate.Section = this.Section;
            materialToUpdate.Comments = this.Comments;

            return materialToUpdate;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("exam_id", Required = Required.Always)]
        public int ExamId { get; }

        [JsonProperty("book_id")]
        public int? BookId { get; }

        [JsonProperty("chapter")]
        public string? Chapter { get; }

        [JsonProperty("section")]
        public string? Section { get; }

        [JsonProperty("comments")]
        public string? Comments { get; }
    }
}
