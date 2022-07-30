using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
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

        public Classroom ToClassroom()
        {
            return new Classroom()
            {
                Id = this.Id,
                SchoolId = this.SchoolId,
                Name = this.Name,
                Comments = this.Comments
            }.Normalize();
        }

        public Classroom ToClassroom(Classroom classroomToUpdate)
        {
            classroomToUpdate.Name = this.Name;
            classroomToUpdate.Comments = this.Comments;

            return classroomToUpdate.Normalize();
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("school_id", Required = Required.Always)]
        public int SchoolId { get; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; } = null!;

        [JsonProperty("comments")]
        public string? Comments { get; }
    }
}
