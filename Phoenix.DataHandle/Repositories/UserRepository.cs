using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class UserRepository : ObviableRepository<UserInfo>
    {
        public UserRepository(PhoenixContext dbContext) 
            : base(dbContext)
        {
            Include(u => u.AspNetUser);
        }

        #region Validate

        public bool IsValid(UserInfo user)
        {
            return user.IsSelfDetermined && user.DependenceOrder != 0;
        }

        public void CheckIfValid(UserInfo user)
        {
            if (!IsValid(user))
                throw new InvalidOperationException($"{nameof(user.DependenceOrder)} must be 0 for a self-determined user.");
        }

        public void CheckIfValid(IEnumerable<UserInfo> users)
        {
            foreach (var user in users)
                CheckIfValid(user);
        }

        #endregion

        #region Create

        public override Task<UserInfo> CreateAsync(UserInfo user,
            CancellationToken cancellationToken = default)
        {
            CheckIfValid(user);
            return base.CreateAsync(user, cancellationToken);
        }

        public override Task<IEnumerable<UserInfo>> CreateRangeAsync(IEnumerable<UserInfo> users,
            CancellationToken cancellationToken = default)
        {
            CheckIfValid(users);
            return base.CreateRangeAsync(users, cancellationToken);
        }

        #endregion

        #region Update

        public override Task<UserInfo> UpdateAsync(UserInfo user,
            CancellationToken cancellationToken = default)
        {
            CheckIfValid(user);
            return base.UpdateAsync(user, cancellationToken);
        }

        public override Task<IEnumerable<UserInfo>> UpdateRangeAsync(IEnumerable<UserInfo> users,
            CancellationToken cancellationToken = default)
        {
            CheckIfValid(users);
            return base.UpdateRangeAsync(users, cancellationToken);
        }

        public UserInfo UpdateWithUserInfo(UserInfo user, IUserInfo userFrom)
        {
            CopyAspNetUser(user.AspNetUser, userFrom.AspNetUser);
            return Update(user, userFrom);
        }

        public async Task<UserInfo> UpdateWithUserInfoAsync(UserInfo user, IUserInfo userFrom,
            CancellationToken cancellationToken = default)
        {
            CopyAspNetUser(user.AspNetUser, userFrom.AspNetUser);
            return await UpdateAsync(user, userFrom, cancellationToken);
        }

        public ApplicationUser CopyAspNetUser(ApplicationUser appUser, IAspNetUser aspNetUserFrom)
        {
            if (aspNetUserFrom is null)
                throw new ArgumentNullException(nameof(aspNetUserFrom));
            if (appUser is null)
                appUser = new();

            PropertyCopier.CopyFromBase(appUser, aspNetUserFrom);

            return appUser;
        }

        #endregion
    }
}
