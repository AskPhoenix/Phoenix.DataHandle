using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class RoleRepository : Repository<Role>
    {
        public RoleRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        public static Expression<Func<Role, bool>> GetUniqueExpression(RoleRank roleRank)
        {
            return r => r.Rank == roleRank;
        }

        #region Find Unique

        public Role? FindUnique(RoleRank roleRank)
        {
            return FindUnique(GetUniqueExpression(roleRank));
        }

        public Role? FindUnique(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));

            return FindUnique(roleName.ToRoleRank());
        }

        public Role? FindUnique(IRole role)
        {
            if (role is null)
                throw new ArgumentNullException(nameof(role));

            return FindUnique(role.Rank);
        }

        public async Task<Role?> FindUniqueAsync(RoleRank roleRank,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(roleRank),
                cancellationToken);
        }

        public async Task<Role?> FindUniqueAsync(string roleName,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));

            return await FindUniqueAsync(roleName.ToRoleRank(),
                cancellationToken);
        }

        public async Task<Role?> FindUniqueAsync(IRole role,
            CancellationToken cancellationToken = default)
        {
            if (role is null)
                throw new ArgumentNullException(nameof(role));

            return await FindUniqueAsync(role.Rank,
                cancellationToken);
        }

        #endregion
    }
}
