using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class ExerciseApi : IExerciseApi, IModelApi
    {
        [JsonConstructor]
        public ExerciseApi(int lectureId, string name, int? bookId, string? page, string? comments)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = null!;

            this.Id = 0;
            this.LectureId = lectureId;
            this.Name = name;
            this.BookId = bookId;
            this.Page = page;
            this.Comments = comments;
        }

        public ExerciseApi(int lectureId, int? bookId, IExerciseBase exercise)
            : this(lectureId, exercise.Name, bookId, exercise.Page, exercise.Comments)
        {
        }

        public ExerciseApi(Exercise exercise)
            : this(exercise.LectureId, exercise.BookId, exercise)
        {
            this.Id = exercise.Id;
        }

        public Exercise ToExercise()
        {
            return new Exercise()
            {
                Id = this.Id,
                LectureId = this.LectureId,
                Name = this.Name,
                BookId = this.BookId,
                Page = this.Page,
                Comments = this.Comments
            };
        }

        public Exercise ToExercise(Exercise exerciseToUpdate)
        {
            exerciseToUpdate.LectureId = this.LectureId;
            exerciseToUpdate.Name = this.Name;
            exerciseToUpdate.BookId = this.BookId;
            exerciseToUpdate.Page = this.Page;
            exerciseToUpdate.Comments = this.Comments;

            return exerciseToUpdate;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("lecture_id", Required = Required.Always)]
        public int LectureId { get; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; } = null!;

        [JsonProperty("book_id")]
        public int? BookId { get; }

        [JsonProperty("page")]
        public string? Page { get; }

        [JsonProperty("comments")]
        public string? Comments { get; }
    }
}
