using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class ExerciseRepository : Repository<Exercise>
    {
        public ExerciseRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        #region Search

        public IQueryable<Exercise> Search(int? lectureId = null, int? bookId = null)
        {
            var exercises = Find();

            if (lectureId.HasValue)
                exercises = exercises.Where(e => e.LectureId == lectureId);
            if (bookId.HasValue)
                exercises = exercises.Where(e => e.BookId == bookId);

            return exercises;
        }

        #endregion
    }
}
