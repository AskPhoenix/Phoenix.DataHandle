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
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            this.Id = id;
            this.School = school;
            this.Name = name;
            this.Comments = comments;
        }

        public ClassroomApi(IClassroom classroom, bool include = false)
            : this(0, null!, classroom.Name, classroom.Comments)
        {
            if (classroom is Classroom classroom1)
                this.Id = classroom1.Id;

            if (!include)
                return;

            if (classroom.School is not null)
                this.School = new SchoolApi(classroom.School);
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
