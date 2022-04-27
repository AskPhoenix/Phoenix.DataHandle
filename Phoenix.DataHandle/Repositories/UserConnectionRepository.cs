using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class UserConnectionRepository : ConnectionRepository<UserConnection>
    {
        private UserRepository userRepository;

        public UserConnectionRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
            this.userRepository = new(phoenixContext);
            userRepository.Include(u => u.Children);
        }

        #region Disconnect Affiliated

        public IEnumerable<UserConnection> DisconnectAffiliated(ChannelProvider channelProvider, int parentId)
        {
            var parent = userRepository.FindPrimary(parentId);
            if (parent is null)
                throw new InvalidOperationException($"There is no User with ID {parentId}.");
            
            var affiliatedIds = parent.Children.Select(u => u.AspNetUserId);
            if (!affiliatedIds.Any())
                return Enumerable.Empty<UserConnection>();

            return DisconnectRange(channelProvider, affiliatedIds);
        }

        public IEnumerable<UserConnection> DisconnectAffiliatedAnywhere(int parentId)
        {
            List<UserConnection> connections = new();

            foreach (var channelProvider in Enum.GetValues<ChannelProvider>())
                connections.AddRange(DisconnectAffiliated(channelProvider, parentId));

            return connections;
        }

        public async Task<IEnumerable<UserConnection>> DisconnectAffiliatedAsync(ChannelProvider channelProvider, int parentId,
            CancellationToken cancellationToken = default)
        {
            var parent = await userRepository.FindPrimaryAsync(parentId, cancellationToken);
            if (parent is null)
                throw new InvalidOperationException($"There is no User with ID {parentId}.");

            var affiliatedIds = parent.Children.Select(u => u.AspNetUserId);
            if (!affiliatedIds.Any())
                return Enumerable.Empty<UserConnection>();

            return await DisconnectRangeAsync(channelProvider, affiliatedIds, cancellationToken);
        }

        public async Task<IEnumerable<UserConnection>> DisconnectAffiliatedAnywhereAsync(int parentId,
            CancellationToken cancellationToken = default)
        {
            List<UserConnection> connections = new();

            foreach (var channelProvider in Enum.GetValues<ChannelProvider>())
                connections.AddRange(await DisconnectAffiliatedAsync(channelProvider, parentId, cancellationToken));

            return connections;
        }

        #endregion
    }
}
