using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Models.Extensions;
using Phoenix.DataHandle.Main.Types;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public abstract class ConnectionRepository<TConnectionModel> : Repository<TConnectionModel>
        where TConnectionModel : class, IConnectionEntity, new()
    {
        private const string InvalidRegisterMsg = "{0} Channel Key {1} is already assigned to another tenant.";
        private const string InvalidConnectionMsg = "{0} Channel Key {1} is not assigned to any tenant.";

        public ConnectionRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        public static Expression<Func<TConnectionModel, bool>> GetUniqueExpression(
            ChannelProvider channelProvider, string channelKey)
        {
            if (string.IsNullOrWhiteSpace(channelKey))
                throw new ArgumentNullException(nameof(channelKey));

            return c => c.Channel == channelProvider.ToString() && c.ChannelKey == channelKey;
        }

        #region Find Unique

        public Task<TConnectionModel?> FindUniqueAsync(
            ChannelProvider channelProvider, string channelKey,
            CancellationToken cancellationToken = default)
        {
            return FindUniqueAsync(GetUniqueExpression(channelProvider, channelKey),
                cancellationToken);
        }

        #endregion

        #region Register

        public virtual async Task<TConnectionModel> RegisterAsync(ChannelProvider channelProvider,
            string channelKey, int tenantId, bool activate = true,
            CancellationToken cancellationToken = default)
        {
            var connection = await FindUniqueAsync(channelProvider, channelKey, cancellationToken);

            if (connection is null)
            {
                connection = new()
                {
                    TenantId = tenantId,
                    Channel = channelProvider.ToString(),
                    ChannelKey = channelKey,
                    ChannelDisplayName = channelProvider.ToFriendlyString(),
                    ActivatedAt = activate ? DateTime.UtcNow : null
                };

                return await CreateAsync(connection, cancellationToken);
            }

            if (connection.TenantId != tenantId)
                throw new InvalidOperationException(
                    string.Format(InvalidRegisterMsg, channelProvider.ToFriendlyString(), channelKey));

            if (activate && !connection.IsActive)
            {
                connection.ActivatedAt = DateTime.UtcNow;
                return await UpdateAsync(connection, cancellationToken);
            }

            if (!activate && connection.IsActive)
            {
                connection.ActivatedAt = null;
                return await UpdateAsync(connection, cancellationToken);
            }

            return connection;
        }

        #endregion

        #region Connect
        
        public virtual async Task<TConnectionModel> ConnectAsync(
            ChannelProvider channelProvider, string channelKey,
            CancellationToken cancellationToken = default)
        {
            var connection = await FindUniqueAsync(channelProvider, channelKey, cancellationToken);

            if (connection is null)
                throw new InvalidOperationException(
                    string.Format(InvalidConnectionMsg, channelProvider.ToFriendlyString(), channelKey));

            // Update ActivatedAt every time the tenant is connected
            connection.ActivatedAt = DateTime.UtcNow;

            return await UpdateAsync(connection, cancellationToken);
        }

        #endregion

        #region Disconnect

        protected TConnectionModel DisconnectPrepare(TConnectionModel connection)
        {
            if (connection is null)
                throw new ArgumentNullException(nameof(connection));

            connection.ActivatedAt = null;

            return connection;
        }

        protected IEnumerable<TConnectionModel> DisconnectRangePrepare(
            IEnumerable<TConnectionModel> connections)
        {
            if (connections is null)
                throw new ArgumentNullException(nameof(connections));

            foreach (var connection in connections)
                DisconnectPrepare(connection);

            return connections;
        }

        public async Task<TConnectionModel> DisconnectAsync(
            TConnectionModel connection,
            CancellationToken cancellationToken = default)
        {
            return await UpdateAsync(DisconnectPrepare(connection), cancellationToken);
        }

        public async Task<TConnectionModel> DisconnectAsync(
            ChannelProvider channelProvider, string channelKey,
            CancellationToken cancellationToken = default)
        {
            var connection = await FindUniqueAsync(channelProvider, channelKey, cancellationToken);
            
            if (connection is null)
                throw new InvalidOperationException(
                    string.Format(InvalidConnectionMsg, channelProvider.ToFriendlyString(), channelKey));

            return await DisconnectAsync(connection, cancellationToken);
        }

        public async Task<IEnumerable<TConnectionModel>> DisconnectRangeAsync(
            IEnumerable<TConnectionModel> connections,
            CancellationToken cancellationToken = default)
        {
            if (connections is null)
                throw new ArgumentNullException(nameof(connections));
            if (!connections.Any())
                return Enumerable.Empty<TConnectionModel>();

            return await UpdateRangeAsync(DisconnectRangePrepare(connections), cancellationToken);
        }

        public Task<IEnumerable<TConnectionModel>> DisconnectFromChannelAsync(
            ChannelProvider channelProvider, int tenantId,
            CancellationToken cancellationToken = default)
        {
            var connections = Find()
                .Where(c => c.Channel == channelProvider.ToString() && c.TenantId == tenantId);

            return DisconnectRangeAsync(connections, cancellationToken);
        }

        public Task<IEnumerable<TConnectionModel>> DisconnectFromAnywhereAsync(
            int tenantId,
            CancellationToken cancellationToken = default)
        {
            var connections = Find().Where(c => c.TenantId == tenantId);

            return DisconnectRangeAsync(connections, cancellationToken);
        }

        public Task<IEnumerable<TConnectionModel>> DisconnectRangeFromChannelAsync(
            ChannelProvider channelProvider, IEnumerable<int> tenantIds,
            CancellationToken cancellationToken = default)
        {
            if (tenantIds is null)
                throw new ArgumentNullException(nameof(tenantIds));

            var connections = Find()
                .Where(c => c.Channel == channelProvider.ToString() && tenantIds.Contains(c.TenantId));

            return DisconnectRangeAsync(connections, cancellationToken);
        }

        public Task<IEnumerable<TConnectionModel>> DisconnectRangeAnywhereAsync(
            IEnumerable<int> tenantIds,
            CancellationToken cancellationToken = default)
        {
            if (tenantIds is null)
                throw new ArgumentNullException(nameof(tenantIds));

            var connections = Find().Where(c => tenantIds.Contains(c.TenantId));

            return DisconnectRangeAsync(connections, cancellationToken);
        }

        #endregion
    }
}
