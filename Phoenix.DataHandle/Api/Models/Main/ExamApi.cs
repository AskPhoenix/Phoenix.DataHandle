using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class ExamApi : IExam, IModelApi
    {
        private ExamApi()
        {
            this.Grades = new List<GradeApi>();
            this.Materials = new List<MaterialApi>();
        }

        [JsonConstructor]
        public ExamApi(int id, LectureApi lecture, string? name, string? comments, List<GradeApi>? grades, List<MaterialApi>? materials)
            : this()
        {
            this.Id = id;
            this.Lecture = lecture;
            this.Name = name;
            this.Comments = comments;

            if (grades is not null)
                this.Grades = grades;
            if (materials is not null)
                this.Materials = materials;
        }

        public ExamApi(IExam exam, bool include = false)
            : this(0, null!, exam.Name, exam.Comments, null, null)
        {
            if (exam is Exam exam1)
                this.Id = exam1.Id;

            if (!include)
                return;

            if (exam.Lecture is not null)
                this.Lecture = new LectureApi(exam.Lecture);

            this.Grades = exam.Grades.Select(g => new GradeApi(g)).ToList();
            this.Materials = exam.Materials.Select(m => new MaterialApi(m)).ToList();
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "lecture")]
        public LectureApi Lecture { get; } = null!;

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        [JsonProperty(PropertyName = "grades")]
        public List<GradeApi> Grades { get; }

        [JsonProperty(PropertyName = "materials")]
        public List<MaterialApi> Materials { get; }
        

        ILecture IExam.Lecture => this.Lecture;

        IEnumerable<IGrade> IExam.Grades => this.Grades;

        IEnumerable<IMaterial> IExam.Materials => this.Materials;
    }
}
