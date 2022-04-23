using Phoenix.DataHandle.Main.Models;
using System.Linq;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class MaterialRepository : Repository<Material>
    {
        public MaterialRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        #region Search

        public IQueryable<Material> Search(int? examId = null, int? bookId = null)
        {
            var materials = Find();

            if (examId.HasValue)
                materials = materials.Where(m => m.ExamId == examId);
            if (bookId.HasValue)
                materials = materials.Where(m => m.BookId == bookId);

            return materials;
        }

        #endregion
    }
}
