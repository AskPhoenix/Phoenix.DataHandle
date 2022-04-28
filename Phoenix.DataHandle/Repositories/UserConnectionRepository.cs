using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class UserConnectionRepository : ConnectionRepository<UserConnection>
    {
        private readonly UserRepository userRepository;

        public UserConnectionRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
            this.userRepository = new(phoenixContext);
            userRepository.Include(u => u.Children);
        }

        #region Disconnect Affiliated

        private async Task<IEnumerable<int>> DisconnectAffiliatedPrepareAsync(
            int parentId,
            CancellationToken cancellationToken = default)
        {
            var parent = await userRepository.FindPrimaryAsync(parentId, cancellationToken);
            if (parent is null)
                throw new InvalidOperationException($"There is no User with ID {parentId}.");

            return parent.Children.Select(u => u.AspNetUserId);
        }

        public async Task<IEnumerable<UserConnection>> DisconnectAffiliatedAsync(
            ChannelProvider channelProvider, int parentId,
            CancellationToken cancellationToken = default)
        {
            var affiliatedIds = await DisconnectAffiliatedPrepareAsync(parentId, cancellationToken);
            if (!affiliatedIds.Any())
                return Enumerable.Empty<UserConnection>();

            return await DisconnectRangeFromChannelAsync(channelProvider, affiliatedIds, cancellationToken);
        }

        public async Task<IEnumerable<UserConnection>> DisconnectAffiliatedAnywhereAsync(
            int parentId,
            CancellationToken cancellationToken = default)
        {
            var affiliatedIds = await DisconnectAffiliatedPrepareAsync(parentId, cancellationToken);
            if (!affiliatedIds.Any())
                return Enumerable.Empty<UserConnection>();


            return await DisconnectRangeAnywhereAsync(affiliatedIds, cancellationToken);
        }

        #endregion
    }
}
