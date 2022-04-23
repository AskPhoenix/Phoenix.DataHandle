using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class UserRepository : ObviableRepository<User>
    {
        public UserRepository(PhoenixContext dbContext) 
            : base(dbContext)
        {
            Include(u => u.UserInfo);
        }

        public static Expression<Func<User, bool>> GetUniqueExpression(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            string normName = User.NormFunc(username);
            return u => u.NormalizedUserName == normName;
        }

        public static Expression<Func<User, bool>> GetUniqueExpression(
            string phoneCountryCode, string phoneNumber, int dependenceOrder)
        {
            if (string.IsNullOrWhiteSpace(phoneCountryCode))
                throw new ArgumentNullException(nameof(phoneCountryCode));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            return u => u.PhoneCountryCode == phoneCountryCode
                        && u.PhoneNumber == phoneNumber
                        && u.DependenceOrder == dependenceOrder;
        }

        #region Find Unique

        public User? FindUnique(string username)
        {
            return FindUnique(GetUniqueExpression(username));
        }

        public User? FindUnique(IUser user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            return FindUnique(user.UserName);
        }

        public User? FindUnique(string phoneCountryCode, string phoneNumber, int dependenceOrder)
        {
            return FindUnique(GetUniqueExpression(phoneCountryCode, phoneNumber, dependenceOrder));
        }

        public async Task<User?> FindUniqueAsync(string username,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(username),
                cancellationToken);
        }

        public async Task<User?> FindUniqueAsync(IUser user,
            CancellationToken cancellationToken = default)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            return await FindUniqueAsync(user.UserName, cancellationToken);
        }

        public async Task<User?> FindUniqueAsync(string phoneCountryCode, string phoneNumber, int dependenceOrder,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(phoneCountryCode, phoneNumber, dependenceOrder));
        }

        #endregion

        #region Update

        public User UpdateWithUserInfo(User user, IUser userFrom)
        {
            CopyUserInfo(user.UserInfo, userFrom.UserInfo);
            return Update(user, userFrom);
        }

        public async Task<User> UpdateWithUserInfoAsync(User user, IUser userFrom,
            CancellationToken cancellationToken = default)
        {
            CopyUserInfo(user.UserInfo, userFrom.UserInfo);
            return await UpdateAsync(user, userFrom, cancellationToken);
        }

        public UserInfo CopyUserInfo(UserInfo userInfo, IUserInfo userInfoFrom)
        {
            if (userInfoFrom is null)
                throw new ArgumentNullException(nameof(userInfoFrom));
            if (userInfo is null)
                userInfo = new();

            PropertyCopier.CopyFromBase(userInfo, userInfoFrom);

            return userInfo;
        }

        #endregion
    }
}
