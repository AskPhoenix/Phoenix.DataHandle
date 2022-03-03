using System;
using System.Collections.Generic;
using System.Linq;
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
            if (lecture is null)
                throw new ArgumentNullException(nameof(lecture));

            this.Id = id;
            this.Lecture = lecture;
            this.Name = name;
            this.Comments = comments;

            if (grades is not null)
                this.Grades = grades;
            if (materials is not null)
                this.Materials = materials;
        }

        public ExamApi(IExam exam, int id = 0)
            : this(id, new LectureApi(exam.Lecture), exam.Name, exam.Comments, null, null)
        {
            this.Grades = exam.Grades.Select(g => new GradeApi(g)).ToList();
            this.Materials = exam.Materials.Select(m => new MaterialApi(m)).ToList();
        }

        public ExamApi(Exam exam) 
            : this(exam, exam.Id)
        {
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
