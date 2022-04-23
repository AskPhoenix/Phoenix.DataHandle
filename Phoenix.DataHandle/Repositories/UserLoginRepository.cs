using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class UserLoginRepository : LoginRepository<UserLogin>
    {
        private UserRepository userRepository;

        public UserLoginRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
            this.userRepository = new(phoenixContext);
            userRepository.Include(u => u.Children);
        }

        #region Logout Affiliated

        public IEnumerable<UserLogin> LogoutAffiliated(ChannelProvider provider, int parentId)
        {
            var parent = userRepository.FindPrimary(parentId);
            if (parent is null)
                throw new InvalidOperationException($"There is no User with ID {parentId}.");
            
            var affiliatedIds = parent.Children.Select(u => u.Id);
            if (!affiliatedIds.Any())
                return Enumerable.Empty<UserLogin>();

            return LogoutRange(provider, affiliatedIds);
        }

        public IEnumerable<UserLogin> LogoutAffiliatedAnywhere(int parentId)
        {
            List<UserLogin> logins = new();

            foreach (var provider in Enum.GetValues<ChannelProvider>())
                logins.AddRange(LogoutAffiliated(provider, parentId));

            return logins;
        }

        public async Task<IEnumerable<UserLogin>> LogoutAffiliatedAsync(ChannelProvider provider, int parentId,
            CancellationToken cancellationToken = default)
        {
            var parent = await userRepository.FindPrimaryAsync(parentId, cancellationToken);
            if (parent is null)
                throw new InvalidOperationException($"There is no User with ID {parentId}.");

            var affiliatedIds = parent.Children.Select(u => u.Id);
            if (!affiliatedIds.Any())
                return Enumerable.Empty<UserLogin>();

            return await LogoutRangeAsync(provider, affiliatedIds, cancellationToken);
        }

        public async Task<IEnumerable<UserLogin>> LogoutAffiliatedAnywhereAsync(int parentId,
            CancellationToken cancellationToken = default)
        {
            List<UserLogin> logins = new();

            foreach (var provider in Enum.GetValues<ChannelProvider>())
                logins.AddRange(await LogoutAffiliatedAsync(provider, parentId, cancellationToken));

            return logins;
        }

        #endregion
    }
}
