using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using System;
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

        #region Update

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
