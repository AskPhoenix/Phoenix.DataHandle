using System;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class MaterialApi : IMaterial, IModelApi
    {
        [JsonConstructor]
        public MaterialApi(int id, ExamApi exam, BookApi? book, string? chapter, string? section, string? comments)
        {
            this.Id = id;
            this.Exam = exam;
            this.Book = book;
            this.Chapter = chapter;
            this.Section = section;
            this.Comments = comments;
        }

        public MaterialApi(IMaterial material, bool include = false)
            : this(0, null!, null, material.Chapter, material.Section, material.Comments)
        {
            if (material is Material material1)
                this.Id = material1.Id;

            if (!include)
                return;

            if (material.Exam is not null)
                this.Exam = new ExamApi(material.Exam);
            if (material.Book is not null)
                this.Book = new BookApi(material.Book);
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "exam")]
        public ExamApi Exam { get; } = null!;

        [JsonProperty(PropertyName = "book")]
        public BookApi? Book { get; }

        [JsonProperty(PropertyName = "chapter")]
        public string? Chapter { get; }

        [JsonProperty(PropertyName = "section")]
        public string? Section { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }


        IExam IMaterial.Exam => this.Exam;

        IBook? IMaterial.Book => this.Book;
    }
}
