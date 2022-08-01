using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Api.Types
{
    public enum ClientRoleRankApi
    {
        Student = RoleRank.Student - RoleHierarchy.ClientRolesBase,
        Parent = RoleRank.Parent - RoleHierarchy.ClientRolesBase
    }

    public static class ClientRoleRankApiExtensions
    {
        public static RoleRank ConvertToRoleRank(ClientRoleRankApi clientRoleRank)
        {
            var roleRank = (RoleRank)(clientRoleRank + RoleHierarchy.ClientRolesBase);

            if (!roleRank.IsClient())
                return RoleRank.None;

            return roleRank;
        }
    }
}
