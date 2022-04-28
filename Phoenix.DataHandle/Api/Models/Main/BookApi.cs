﻿using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class BookApi : IBook, IModelApi
    {
        private BookApi()
        {
            this.Courses = new List<ICourse>();
            this.Exercises = new List<IExercise>();
            this.Materials = new List<IMaterial>();
        }

        [JsonConstructor]
        public BookApi(int id, string name, string? publisher, string? comments)
            : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            this.Id = id;
            this.Name = name;
            this.Publisher = publisher;
            this.Comments = comments;
        }

        public BookApi(IBook book)
            : this(0, book.Name, book.Publisher, book.Comments)
        {
            if (book is Book book1)
                this.Id = book1.Id;
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "publisher")]
        public string? Publisher { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }


        [JsonIgnore]
        public IEnumerable<ICourse> Courses { get; }

        [JsonIgnore]
        public IEnumerable<IExercise> Exercises { get; }

        [JsonIgnore]
        public IEnumerable<IMaterial> Materials { get; }
    }
}
