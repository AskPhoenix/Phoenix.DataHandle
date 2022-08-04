using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories.Extensions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class UserRepository : ObviableRepository<User>,
        ISetNullDeleteRule<User>, ICascadeDeleteRule<User>
    {
        public UserRepository(PhoenixContext dbContext) 
            : base(dbContext)
        {
        }

        public UserRepository(PhoenixContext dbContext, bool nonObviatedOnly)
            : base(dbContext, nonObviatedOnly)
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

        #region Delete

        public override async Task<User> DeleteAsync(int id,
            CancellationToken cancellationToken = default)
        {
            User? toRemove = await FindPrimaryAsync(id, cancellationToken);
            if (toRemove is null)
                throw new InvalidOperationException($"There is no entry with id {id}.");

            return await DeleteAsync(toRemove, cancellationToken);
        }

        public void SetNullOnDelete(User user)
        {
            user.Lectures.Clear();
            user.BotFeedbacks.Clear();
            user.Broadcasts.Clear();
            user.Courses.Clear();
            user.Children.Clear();
            user.Parents.Clear();
            user.Schools.Clear();
        }

        public async Task CascadeOnDeleteAsync(User user,
            CancellationToken cancellationToken = default)
        {
            await new GradeRepository(DbContext).DeleteRangeAsync(user.Grades, cancellationToken);
            await new OneTimeCodeRepository(DbContext).DeleteRangeAsync(user.OneTimeCodes, cancellationToken);
            await new UserConnectionRepository(DbContext).DeleteRangeAsync(user.UserConnections, cancellationToken);
        }

        public async Task CascadeRangeOnDeleteAsync(IEnumerable<User> users,
            CancellationToken cancellationToken = default)
        {
            await new GradeRepository(DbContext).DeleteRangeAsync(users.SelectMany(u => u.Grades),
                cancellationToken);
            await new OneTimeCodeRepository(DbContext).DeleteRangeAsync(users.SelectMany(u => u.OneTimeCodes),
                cancellationToken);
            await new UserConnectionRepository(DbContext).DeleteRangeAsync(users.SelectMany(u => u.UserConnections),
                cancellationToken);
        }

        #endregion
    }
}
