using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class ExerciseApi : IExercise, IModelApi
    {
        private ExerciseApi()
        {
            this.Grades = new List<GradeApi>();
        }

        [JsonConstructor]
        public ExerciseApi(int id, LectureApi lecture, string name, BookApi? book, string? page, string? comments, List<GradeApi>? grades)
            : this()
        {
            if (lecture is null)
                throw new ArgumentNullException(nameof(lecture));
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            this.Id = id;
            this.Lecture = lecture;
            this.Name = name;
            this.Book = book;
            this.Page = page;
            this.Comments = comments;

            if (grades is not null)
                this.Grades = grades;
        }

        public ExerciseApi(IExercise exercise, int id = 0)
            : this(id, new LectureApi(exercise.Lecture), exercise.Name, null, exercise.Page, exercise.Comments, null)
        {
            if (exercise.Book is not null)
                this.Book = new BookApi(exercise.Book);

            this.Grades = exercise.Grades.Select(g => new GradeApi(g)).ToList();
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "lecture")]
        public LectureApi Lecture { get; } = null!;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "book")]
        public BookApi? Book { get; }

        [JsonProperty(PropertyName = "page")]
        public string? Page { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        [JsonProperty(PropertyName = "grades")]
        public List<GradeApi> Grades { get; }
        
        
        IBook? IExercise.Book => this.Book;
        
        ILecture IExercise.Lecture => this.Lecture;

        IEnumerable<IGrade> IExercise.Grades => this.Grades;
    }
}
