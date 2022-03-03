using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class ClassroomApi : IClassroom, IModelApi
    {
        private ClassroomApi()
        {
            this.Lectures = new List<ILecture>();
            this.Schedules = new List<ISchedule>();
        }

        [JsonConstructor]
        public ClassroomApi(int id, SchoolApi school, string name, string? comments)
            : this()
        {
            // TODO: Make school property nullable?
            if (school is null)
                throw new ArgumentNullException(nameof(school));

            this.Id = id;
            this.School = school;
            this.Name = name;
            this.Comments = comments;
        }

        // TODO: Check if classroom can be null in main db model
        public ClassroomApi(IClassroom classroom, int id = 0)
            : this(id, new SchoolApi(classroom.School), classroom.Name, classroom.Comments)
        {
        }

        public ClassroomApi(Classroom classroom)
            : this(classroom, classroom.Id)
        {
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "school")]
        public SchoolApi School { get; } = null!;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }


        ISchool IClassroom.School => this.School;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; }
        
        [JsonIgnore]
        public IEnumerable<ISchedule> Schedules { get; }
    }
}
