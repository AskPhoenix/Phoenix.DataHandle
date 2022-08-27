using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class SchoolConnectionRepository : ConnectionRepository<SchoolConnection>
    {
        public SchoolConnectionRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        #region Register

        public async Task<SchoolConnection> RegisterAsync(ChannelProvider channelProvider,
            string channelKey, string? channelToken, int tenantId, bool activate = true,
            CancellationToken cancellationToken = default)
        {
            var schoolConnection =
                await RegisterAsync(channelProvider, channelKey, tenantId, activate, cancellationToken);

            schoolConnection.ChannelToken = channelToken;

            return await UpdateAsync(schoolConnection, cancellationToken);
        }

        #endregion
    }
}
