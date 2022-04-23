using Phoenix.DataHandle.Main.Models;
using System.Linq;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class ExamRepository : Repository<Exam>
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
    }
}
