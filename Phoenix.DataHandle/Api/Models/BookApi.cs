using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class BookApi : IBookApi, IModelApi
    {
        [JsonConstructor]
        public BookApi(int id, int schoolId, string name, string? publisher, string? comments)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = null!;

            this.Id = id;
            this.SchoolId = schoolId;
            this.Name = name;
            this.Publisher = publisher;
            this.Comments = comments;
        }

        public BookApi(int id, int schoolId, IBookBase book)
            : this(id, schoolId, book.Name, book.Publisher, book.Comments)
        {
        }

        public BookApi(Book book)
            : this(book.Id, book.SchoolId, book)
        {
        }

        public Book ToBook()
        {
            return new Book()
            {
                Id = this.Id,
                SchoolId = this.SchoolId,
                Name = this.Name,
                Publisher = this.Publisher,
                Comments = this.Comments
            }.Normalize();
        }

        public Book ToBook(Book bookToUpdate)
        {
            bookToUpdate.Name = this.Name;
            bookToUpdate.Publisher = this.Publisher;
            bookToUpdate.Comments = this.Comments;

            return bookToUpdate.Normalize();
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("school_id", Required = Required.Always)]
        public int SchoolId { get; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; } = null!;

        [JsonProperty("publisher")]
        public string? Publisher { get; }

        [JsonProperty("comments")]
        public string? Comments { get; }
    }
}
