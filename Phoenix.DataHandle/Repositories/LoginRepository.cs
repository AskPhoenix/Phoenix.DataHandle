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
    public abstract class LoginRepository<TLoginModel> : Repository<TLoginModel>
        where TLoginModel : class, ILoginEntity, new()
    {
        protected readonly ChannelRepository channelRepository;

        private const string InvalidRegisterMsg = "{0} Provider Key {1} is already assigned to another tenant.";
        private const string InvalidLoginMsg = "{0} Provider Key {1} is not assigned to any tenant.";

        public LoginRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
            this.channelRepository = new(phoenixContext);
        }

        public static Expression<Func<TLoginModel, bool>> GetUniqueExpression(
            ChannelProvider provider, string providerKey)
        {
            if (string.IsNullOrWhiteSpace(providerKey))
                throw new ArgumentNullException(nameof(providerKey));

            return l => l.Channel.Provider == provider && l.ProviderKey == providerKey;
        }

        public static Expression<Func<TLoginModel, bool>> GetUniqueExpression(
            ChannelProvider provider, int tenantId)
        {
            return l => l.Channel.Provider == provider && l.TenantId == tenantId;
        }

        #region Find Unique

        public TLoginModel? FindUnique(ChannelProvider provider, string providerKey)
        {
            return FindUnique(GetUniqueExpression(provider, providerKey));
        }

        public TLoginModel? FindUnique(ChannelProvider provider, int tenantId)
        {
            return FindUnique(GetUniqueExpression(provider, tenantId));
        }

        public async Task<TLoginModel?> FindUniqueAsync(ChannelProvider provider, string providerKey,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(provider, providerKey),
                cancellationToken);
        }

        public async Task<TLoginModel?> FindUniqueAsync(ChannelProvider provider, int tenantId,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(provider, tenantId),
                cancellationToken);
        }

        #endregion

        #region Register

        public virtual TLoginModel Register(ChannelProvider provider, string providerKey,
            int tenantId, bool activate = true)
        {
            var login = FindUnique(provider, providerKey);

            if (login is null)
            {
                Channel? channel = channelRepository.FindUnique(provider);
                if (channel is null)
                    throw new ArgumentOutOfRangeException(nameof(provider));

                login = new()
                {
                    TenantId = tenantId,
                    ChannelId = channel.Id,
                    ProviderKey = providerKey,
                    ActivatedAt = activate ? DateTime.UtcNow : null
                };

                return Create(login);
            }

            if (login.TenantId != tenantId)
                throw new InvalidOperationException(string.Format(InvalidRegisterMsg, provider, providerKey));

            if (activate && !login.IsActive)
            {
                login.ActivatedAt = DateTime.UtcNow;

                return Update(login);
            }

            return login;
        }

        public virtual async Task<TLoginModel> RegisterAsync(ChannelProvider provider, string providerKey,
            int tenantId, bool activate = true,
            CancellationToken cancellationToken = default)
        {
            var login = await FindUniqueAsync(provider, providerKey, cancellationToken);

            if (login is null)
            {
                Channel? channel = await channelRepository.FindUniqueAsync(provider, cancellationToken);
                if (channel is null)
                    throw new ArgumentOutOfRangeException(nameof(provider));

                login = new()
                {
                    TenantId = tenantId,
                    ChannelId = channel.Id,
                    ProviderKey = providerKey,
                    ActivatedAt = activate ? DateTime.UtcNow : null
                };

                return await CreateAsync(login, cancellationToken);
            }

            if (login.TenantId != tenantId)
                throw new InvalidOperationException(string.Format(InvalidRegisterMsg, provider, providerKey));

            if (activate && !login.IsActive)
            {
                login.ActivatedAt = DateTime.UtcNow;

                return await UpdateAsync(login, cancellationToken);
            }

            return login;
        }

        #endregion

        #region Log in
        
        public virtual TLoginModel Login(ChannelProvider provider, string providerKey)
        {
            var login = FindUnique(provider, providerKey);

            if (login is null)
                throw new InvalidOperationException(string.Format(InvalidLoginMsg, provider, providerKey));

            if (login.IsActive)
                return login;

            login.ActivatedAt = DateTime.UtcNow;

            return Update(login);
        }

        public virtual async Task<TLoginModel> LoginAsync(ChannelProvider provider, string providerKey,
            CancellationToken cancellationToken = default)
        {
            var login = await FindUniqueAsync(provider, providerKey, cancellationToken);

            if (login is null)
                throw new InvalidOperationException(string.Format(InvalidLoginMsg, provider, providerKey));

            if (login.IsActive)
                return login;

            login.ActivatedAt = DateTime.UtcNow;

            return await UpdateAsync(login, cancellationToken);
        }

        #endregion

        #region Log out

        protected TLoginModel LogoutPrepare(TLoginModel login)
        {
            if (login is null)
                throw new ArgumentNullException(nameof(login));

            login.ActivatedAt = null;

            return login;
        }

        protected IEnumerable<TLoginModel> LogoutRangePrepare(IEnumerable<TLoginModel> logins)
        {
            if (logins is null)
                throw new ArgumentNullException(nameof(logins));

            return logins.Select(l => LogoutPrepare(l));
        }

        public TLoginModel? Logout(TLoginModel? login)
        {
            if (login is null)
                return null;

            return Update(LogoutPrepare(login));
        }

        public TLoginModel? Logout(ChannelProvider provider, string providerKey)
        {
            return Logout(FindUnique(provider, providerKey));
        }

        public TLoginModel? Logout(ChannelProvider provider, int tenantId)
        {
            return Logout(FindUnique(provider, tenantId));
        }

        public IEnumerable<TLoginModel> LogoutAnywhere(int tenantId)
        {
            var providers = Enum.GetValues<ChannelProvider>();
            List<TLoginModel> logins = new(providers.Length);
            TLoginModel? login;

            foreach (var provider in providers)
            {
                login = Logout(provider, tenantId);

                if (login is not null)
                    logins.Add(login);
            }

            return logins;
        }

        public IEnumerable<TLoginModel> LogoutRange(IEnumerable<TLoginModel> logins)
        {
            if (logins is null)
                throw new ArgumentNullException(nameof(logins));
            if (!logins.Any())
                return Enumerable.Empty<TLoginModel>();

            return UpdateRange(LogoutRangePrepare(logins));
        }

        public IEnumerable<TLoginModel> LogoutRange(ChannelProvider provider, IEnumerable<int> tenantIds)
        {
            List<TLoginModel> logins = new(tenantIds.Count());
            TLoginModel? login;

            foreach (int tenantId in tenantIds)
            {
                login = FindUnique(provider, tenantId);

                if (login is not null)
                    logins.Add(login);
            }

            return LogoutRange(logins);
        }

        public IEnumerable<TLoginModel> LogoutRangeAnywhere(IEnumerable<int> tenantIds)
        {
            var providers = Enum.GetValues<ChannelProvider>();
            List<TLoginModel> logins = new(tenantIds.Count() * providers.Length);

            foreach (var provider in providers)
                logins.AddRange(LogoutRange(provider, tenantIds));

            return logins;
        }

        public async Task<TLoginModel?> LogoutAsync(TLoginModel? login,
            CancellationToken cancellationToken = default)
        {
            if (login is null)
                return null;

            return await UpdateAsync(LogoutPrepare(login), cancellationToken);
        }

        public async Task<TLoginModel?> LogoutAsync(ChannelProvider provider, string providerKey,
            CancellationToken cancellationToken = default)
        {
            var login = await FindUniqueAsync(provider, providerKey, cancellationToken);
            return await LogoutAsync(login, cancellationToken);
        }

        public async Task<TLoginModel?> LogoutAsync(ChannelProvider provider, int tenantId,
            CancellationToken cancellationToken = default)
        {
            var login = await FindUniqueAsync(provider, tenantId, cancellationToken);
            return await LogoutAsync(login, cancellationToken);
        }

        public async Task<IEnumerable<TLoginModel>> LogoutAnywhereAsync(int tenantId,
            CancellationToken cancellationToken = default)
        {
            var providers = Enum.GetValues<ChannelProvider>();
            List<TLoginModel> logins = new(providers.Length);
            TLoginModel? login;

            foreach (var provider in providers)
            {
                login = await LogoutAsync(provider, tenantId, cancellationToken);

                if (login is not null)
                    logins.Add(login);
            }

            return logins;
        }

        public async Task<IEnumerable<TLoginModel>> LogoutRangeAsync(IEnumerable<TLoginModel> logins,
            CancellationToken cancellationToken = default)
        {
            if (logins is null)
                throw new ArgumentNullException(nameof(logins));
            if (!logins.Any())
                return Enumerable.Empty<TLoginModel>();

            return await UpdateRangeAsync(LogoutRangePrepare(logins), cancellationToken);
        }

        public async Task<IEnumerable<TLoginModel>> LogoutRangeAsync(ChannelProvider provider, IEnumerable<int> tenantIds,
            CancellationToken cancellationToken = default)
        {
            List<TLoginModel> logins = new(tenantIds.Count());
            TLoginModel? login;

            foreach (int tenantId in tenantIds)
            {
                login = await FindUniqueAsync(provider, tenantId, cancellationToken);

                if (login is not null)
                    logins.Add(login);
            }

            return await LogoutRangeAsync(logins, cancellationToken);
        }

        public async Task<IEnumerable<TLoginModel>> LogoutRangeAnywhereAsync(IEnumerable<int> tenantIds,
            CancellationToken cancellationToken = default)
        {
            var providers = Enum.GetValues<ChannelProvider>();
            List<TLoginModel> logins = new(tenantIds.Count() * providers.Length);

            foreach (var provider in providers)
                logins.AddRange(await LogoutRangeAsync(provider, tenantIds, cancellationToken));

            return logins;
        }

        #endregion
    }
}
