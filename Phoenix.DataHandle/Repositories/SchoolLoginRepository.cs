using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class SchoolLoginRepository : LoginRepository<SchoolLogin>
    {
        public SchoolLoginRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }
    }
}
