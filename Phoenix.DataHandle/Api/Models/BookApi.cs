﻿using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class BookApi : IBookApi, IModelApi
    {
        [JsonConstructor]
        public BookApi(int id, string name, string? publisher, string? comments)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            this.Id = id;
            this.Name = name;
            this.Publisher = publisher;
            this.Comments = comments;
        }

        public BookApi(int id, IBookBase book)
            : this(id, book.Name, book.Publisher, book.Comments)
        {
        }

        public BookApi(Book book)
            : this(book.Id, book)
        {
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "publisher")]
        public string? Publisher { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }
    }
}
