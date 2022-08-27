using Phoenix.DataHandle.Main.Models;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class DevRegistrationRepository : Repository<DevRegistration>
    {
        public DevRegistrationRepository(PhoenixContext dbContext)
            : base(dbContext)
        {
        }

        public static Expression<Func<DevRegistration, bool>> GetUniqueExpression(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            return dr => dr.Email.Equals(email);
        }

        #region Find Unique

        public Task<DevRegistration?> FindUniqueAsync(string email,
            CancellationToken cancellationToken = default)
        {
            return FindUniqueAsync(GetUniqueExpression(email),
                cancellationToken);
        }

        #endregion
    }
}
