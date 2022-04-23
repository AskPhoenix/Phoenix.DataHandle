using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class ChannelRepository : Repository<Channel>
    {
        public ChannelRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        public static Expression<Func<Channel, bool>> GetUniqueExpression(ChannelProvider provider)
        {
            return c => c.Provider == provider;
        }

        #region Find Unique

        public Channel? FindUnique(ChannelProvider provider)
        {
            return FindUnique(GetUniqueExpression(provider));
        }

        public Channel? FindUnique(string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName))
                throw new ArgumentNullException(nameof(providerName));

            return FindUnique(providerName.ToChannelProvider());
        }

        public Channel? FindUnique(IChannel channel)
        {
            if (channel is null)
                throw new ArgumentNullException(nameof(channel));

            return FindUnique(channel.Provider);
        }

        public async Task<Channel?> FindUniqueAsync(ChannelProvider provider,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(provider),
                cancellationToken);
        }

        public async Task<Channel?> FindUniqueAsync(string providerName,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(providerName))
                throw new ArgumentNullException(nameof(providerName));

            return await FindUniqueAsync(providerName.ToChannelProvider(),
                cancellationToken);
        }

        public async Task<Channel?> FindUniqueAsync(IChannel channel,
            CancellationToken cancellationToken = default)
        {
            if (channel is null)
                throw new ArgumentNullException(nameof(channel));

            return await FindUniqueAsync(channel.Provider,
                cancellationToken);
        }

        #endregion
    }
}
