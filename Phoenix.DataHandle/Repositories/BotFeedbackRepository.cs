using Phoenix.DataHandle.Main.Models;
using System.Linq;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class BotFeedbackRepository : Repository<BotFeedback>
    {
        public BotFeedbackRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        #region Search

        public IQueryable<BotFeedback> Search(int authorId)
        {
            return this.Find().Where(bf => bf.AuthorId == authorId);
        }

        #endregion
    }
}
