using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories.Extensions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class ExamRepository : Repository<Exam>,
        ICascadeDeleteRule<Exam>
    {
        public ExamRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        #region Search

        public IQueryable<Exam> Search(int lectureId)
        {
            return Find().Where(e => e.LectureId == lectureId);
        }

        #endregion

        #region Delete

        public async Task CascadeOnDeleteAsync(Exam exam,
            CancellationToken cancellationToken = default)
        {
            await new GradeRepository(DbContext).DeleteRangeAsync(exam.Grades, cancellationToken);
            await new MaterialRepository(DbContext).DeleteRangeAsync(exam.Materials, cancellationToken);
        }

        public async Task CascadeRangeOnDeleteAsync(IEnumerable<Exam> exams,
            CancellationToken cancellationToken = default)
        {
            await new GradeRepository(DbContext).DeleteRangeAsync(exams.SelectMany(e => e.Grades),
                cancellationToken);
            await new MaterialRepository(DbContext).DeleteRangeAsync(exams.SelectMany(e => e.Materials),
                cancellationToken);
        }

        #endregion
    }
}
