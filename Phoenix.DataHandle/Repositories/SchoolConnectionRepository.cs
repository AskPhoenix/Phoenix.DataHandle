using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class SchoolConnectionRepository : ConnectionRepository<SchoolConnection>
    {
        public SchoolConnectionRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }
    }
}
