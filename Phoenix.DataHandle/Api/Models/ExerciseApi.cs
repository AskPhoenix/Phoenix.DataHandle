using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class ExerciseApi : IExerciseApi, IModelApi
    {
        [JsonConstructor]
        public ExerciseApi(int id, int lectureId, string name, int? bookId, string? page, string? comments)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            this.Id = id;
            this.LectureId = lectureId;
            this.Name = name;
            this.BookId = bookId;
            this.Page = page;
            this.Comments = comments;
        }

        public ExerciseApi(int id, int lectureId, int? bookId, IExerciseBase exercise)
            : this(id, lectureId, exercise.Name, bookId, exercise.Page, exercise.Comments)
        {
        }

        public ExerciseApi(Exercise exercise)
            : this(exercise.Id, exercise.LectureId, exercise.BookId, exercise)
        {
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "lecture_id")]
        public int LectureId { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "book_id")]
        public int? BookId { get; }

        [JsonProperty(PropertyName = "page")]
        public string? Page { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }
    }
}
