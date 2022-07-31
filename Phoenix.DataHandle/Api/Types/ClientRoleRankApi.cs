using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Api.Types
{
    public enum ClientRoleRankApi
    {
        Student = RoleRank.Student - RoleHierarchy.ClientRolesBase,
        Parent = RoleRank.Parent - RoleHierarchy.ClientRolesBase
    }
}
