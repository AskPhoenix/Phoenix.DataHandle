using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Models.Extensions;
using Phoenix.DataHandle.Main.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

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

        public static Expression<Func<TConnectionModel, bool>> GetUniqueExpression(
            ChannelProvider channelProvider, int tenantId)
        {
            return c => c.Channel == channelProvider.ToString() && c.TenantId == tenantId;
        }

        #region Find Unique

        public TConnectionModel? FindUnique(ChannelProvider channelProvider, string channelKey)
        {
            return FindUnique(GetUniqueExpression(channelProvider, channelKey));
        }

        public TConnectionModel? FindUnique(ChannelProvider channelProvider, int tenantId)
        {
            return FindUnique(GetUniqueExpression(channelProvider, tenantId));
        }

        public async Task<TConnectionModel?> FindUniqueAsync(ChannelProvider channelProvider, string channelKey,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(channelProvider, channelKey),
                cancellationToken);
        }

        public async Task<TConnectionModel?> FindUniqueAsync(ChannelProvider channelProvider, int tenantId,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(channelProvider, tenantId),
                cancellationToken);
        }

        #endregion

        #region Register

        public virtual TConnectionModel Register(ChannelProvider channelProvider, string channelKey,
            int tenantId, bool activate = true)
        {
            var connection = FindUnique(channelProvider, channelKey);

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

                return Create(connection);
            }

            if (connection.TenantId != tenantId)
                throw new InvalidOperationException(string.Format(InvalidRegisterMsg, channelProvider, channelKey));

            if (activate && !connection.IsActive)
            {
                connection.ActivatedAt = DateTime.UtcNow;

                return Update(connection);
            }

            return connection;
        }

        public virtual async Task<TConnectionModel> RegisterAsync(ChannelProvider channelProvider, string channelKey,
            int tenantId, bool activate = true,
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
                throw new InvalidOperationException(string.Format(InvalidRegisterMsg, channelProvider, channelKey));

            if (activate && !connection.IsActive)
            {
                connection.ActivatedAt = DateTime.UtcNow;

                return await UpdateAsync(connection, cancellationToken);
            }

            return connection;
        }

        #endregion

        #region Connect
        
        public virtual TConnectionModel Connect(ChannelProvider channelProvider, string channelKey)
        {
            var connection = FindUnique(channelProvider, channelKey);

            if (connection is null)
                throw new InvalidOperationException(string.Format(InvalidConnectionMsg, channelProvider, channelKey));

            if (connection.IsActive)
                return connection;

            connection.ActivatedAt = DateTime.UtcNow;

            return Update(connection);
        }

        public virtual async Task<TConnectionModel> ConnectAsync(ChannelProvider channelProvider, string channelKey,
            CancellationToken cancellationToken = default)
        {
            var connection = await FindUniqueAsync(channelProvider, channelKey, cancellationToken);

            if (connection is null)
                throw new InvalidOperationException(string.Format(InvalidConnectionMsg, channelProvider, channelKey));

            if (connection.IsActive)
                return connection;

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

        protected IEnumerable<TConnectionModel> DisconnectRangePrepare(IEnumerable<TConnectionModel> connections)
        {
            if (connections is null)
                throw new ArgumentNullException(nameof(connections));

            return connections.Select(l => DisconnectPrepare(l));
        }

        public TConnectionModel? Disconnect(TConnectionModel? connection)
        {
            if (connection is null)
                return null;

            return Update(DisconnectPrepare(connection));
        }

        public TConnectionModel? Disconnect(ChannelProvider channelProvider, string providerKey)
        {
            return Disconnect(FindUnique(channelProvider, providerKey));
        }

        public TConnectionModel? Disconnect(ChannelProvider channelProvider, int tenantId)
        {
            return Disconnect(FindUnique(channelProvider, tenantId));
        }

        public IEnumerable<TConnectionModel> DisconnectAnywhere(int tenantId)
        {
            var channelProviders = Enum.GetValues<ChannelProvider>();
            List<TConnectionModel> connections = new(channelProviders.Length);
            TConnectionModel? connection;

            foreach (var channelProvider in channelProviders)
            {
                connection = Disconnect(channelProvider, tenantId);

                if (connection is not null)
                    connections.Add(connection);
            }

            return connections;
        }

        public IEnumerable<TConnectionModel> DisconnectRange(IEnumerable<TConnectionModel> connections)
        {
            if (connections is null)
                throw new ArgumentNullException(nameof(connections));
            if (!connections.Any())
                return Enumerable.Empty<TConnectionModel>();

            return UpdateRange(DisconnectRangePrepare(connections));
        }

        public IEnumerable<TConnectionModel> DisconnectRange(ChannelProvider channelProvider, IEnumerable<int> tenantIds)
        {
            List<TConnectionModel> connections = new(tenantIds.Count());
            TConnectionModel? connection;

            foreach (int tenantId in tenantIds)
            {
                connection = FindUnique(channelProvider, tenantId);

                if (connection is not null)
                    connections.Add(connection);
            }

            return DisconnectRange(connections);
        }

        public IEnumerable<TConnectionModel> DisconnectRangeAnywhere(IEnumerable<int> tenantIds)
        {
            var channelProviders = Enum.GetValues<ChannelProvider>();
            List<TConnectionModel> connections = new(tenantIds.Count() * channelProviders.Length);

            foreach (var channelProvider in channelProviders)
                connections.AddRange(DisconnectRange(channelProvider, tenantIds));

            return connections;
        }

        public async Task<TConnectionModel?> DisconnectAsync(TConnectionModel? connection,
            CancellationToken cancellationToken = default)
        {
            if (connection is null)
                return null;

            return await UpdateAsync(DisconnectPrepare(connection), cancellationToken);
        }

        public async Task<TConnectionModel?> DisconnectAsync(ChannelProvider channelProvider, string providerKey,
            CancellationToken cancellationToken = default)
        {
            var connection = await FindUniqueAsync(channelProvider, providerKey, cancellationToken);
            return await DisconnectAsync(connection, cancellationToken);
        }

        public async Task<TConnectionModel?> DisconnectAsync(ChannelProvider channelProvider, int tenantId,
            CancellationToken cancellationToken = default)
        {
            var connection = await FindUniqueAsync(channelProvider, tenantId, cancellationToken);
            return await DisconnectAsync(connection, cancellationToken);
        }

        public async Task<IEnumerable<TConnectionModel>> DisconnectAnywhereAsync(int tenantId,
            CancellationToken cancellationToken = default)
        {
            var channelProviders = Enum.GetValues<ChannelProvider>();
            List<TConnectionModel> connections = new(channelProviders.Length);
            TConnectionModel? connection;

            foreach (var channelProvider in channelProviders)
            {
                connection = await DisconnectAsync(channelProvider, tenantId, cancellationToken);

                if (connection is not null)
                    connections.Add(connection);
            }

            return connections;
        }

        public async Task<IEnumerable<TConnectionModel>> DisconnectRangeAsync(IEnumerable<TConnectionModel> connections,
            CancellationToken cancellationToken = default)
        {
            if (connections is null)
                throw new ArgumentNullException(nameof(connections));
            if (!connections.Any())
                return Enumerable.Empty<TConnectionModel>();

            return await UpdateRangeAsync(DisconnectRangePrepare(connections), cancellationToken);
        }

        public async Task<IEnumerable<TConnectionModel>> DisconnectRangeAsync(ChannelProvider channelProvider, IEnumerable<int> tenantIds,
            CancellationToken cancellationToken = default)
        {
            List<TConnectionModel> connections = new(tenantIds.Count());
            TConnectionModel? connection;

            foreach (int tenantId in tenantIds)
            {
                connection = await FindUniqueAsync(channelProvider, tenantId, cancellationToken);

                if (connection is not null)
                    connections.Add(connection);
            }

            return await DisconnectRangeAsync(connections, cancellationToken);
        }

        public async Task<IEnumerable<TConnectionModel>> DisconnectRangeAnywhereAsync(IEnumerable<int> tenantIds,
            CancellationToken cancellationToken = default)
        {
            var channelProviders = Enum.GetValues<ChannelProvider>();
            List<TConnectionModel> connections = new(tenantIds.Count() * channelProviders.Length);

            foreach (var channelProvider in channelProviders)
                connections.AddRange(await DisconnectRangeAsync(channelProvider, tenantIds, cancellationToken));

            return connections;
        }

        #endregion
    }
}
