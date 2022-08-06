using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class DevRegistrationRepository : Repository<DevRegistration>
    {
        public DevRegistrationRepository(PhoenixContext dbContext)
            : base(dbContext)
        {
        }

        #region Search

        public IQueryable<DevRegistration> Search(string email)
        {
            if (email is null)
                throw new ArgumentNullException(nameof(email));

            return this.Find().Where(dr => dr.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        #endregion
    }
}
