﻿using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class ClassroomApi : IClassroomApi, IModelApi
    {
        [JsonConstructor]
        public ClassroomApi(int id, int schoolId, string name, string? comments)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            this.Id = id;
            this.SchoolId = schoolId;
            this.Name = name;
            this.Comments = comments;
        }

        public ClassroomApi(int id, int schoolId, IClassroomBase classroom)
            : this(id, schoolId, classroom.Name, classroom.Comments)
        {
        }

        public ClassroomApi(Classroom classroom)
            : this(0, 0, classroom)
        {
            Id = classroom.Id;
            SchoolId = classroom.SchoolId;
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "school_id")]
        public int SchoolId { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }
    }
}
