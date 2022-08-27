using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories.Extensions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class ExerciseRepository : Repository<Exercise>,
        ICascadeDeleteRule<Exercise>
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

        #region Delete

        public async Task CascadeOnDeleteAsync(Exercise exercise,
            CancellationToken cancellationToken = default)
        {
            await new GradeRepository(DbContext).DeleteRangeAsync(exercise.Grades, cancellationToken);
        }

        public async Task CascadeRangeOnDeleteAsync(IEnumerable<Exercise> exercises,
            CancellationToken cancellationToken = default)
        {
            await new GradeRepository(DbContext).DeleteRangeAsync(exercises.SelectMany(e => e.Grades),
                cancellationToken);
        }

        #endregion
    }
}
