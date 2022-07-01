using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class UserRepository : ObviableRepository<User>
    {
        public UserRepository(PhoenixContext dbContext) 
            : base(dbContext)
        {
        }

        #region Validate

        public bool IsValid(User user)
        {
            return (user.IsSelfDetermined && user.DependenceOrder == 0)
                ||(!user.IsSelfDetermined && user.DependenceOrder > 0);
        }

        public void CheckIfValid(User user)
        {
            if (!IsValid(user))
                throw new InvalidOperationException(
                    $"{nameof(user.DependenceOrder)} must be 0 for a self-determined user.");
        }

        public void CheckRangeIfValid(IEnumerable<User> users)
        {
            foreach (var user in users)
                CheckIfValid(user);
        }

        #endregion

        #region Find

        public new Task<User?> FindPrimaryAsync(int aspNetUserId,
            CancellationToken cancellationToken = default)
        {
            return Find()
                .SingleOrDefaultAsync(a => a.AspNetUserId == aspNetUserId, cancellationToken);
        }

        #endregion

        #region Create

        public override Task<User> CreateAsync(User user,
            CancellationToken cancellationToken = default)
        {
            CheckIfValid(user);
            return base.CreateAsync(user, cancellationToken);
        }

        public override Task<IEnumerable<User>> CreateRangeAsync(IEnumerable<User> users,
            CancellationToken cancellationToken = default)
        {
            CheckRangeIfValid(users);
            return base.CreateRangeAsync(users, cancellationToken);
        }

        #endregion

        #region Update

        public override Task<User> UpdateAsync(User user,
            CancellationToken cancellationToken = default)
        {
            CheckIfValid(user);
            return base.UpdateAsync(user, cancellationToken);
        }

        public override Task<IEnumerable<User>> UpdateRangeAsync(IEnumerable<User> users,
            CancellationToken cancellationToken = default)
        {
            CheckRangeIfValid(users);
            return base.UpdateRangeAsync(users, cancellationToken);
        }

        #endregion
    }
}
