using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Api.Types
{
    public enum PersonnelRoleRankApi
    {
        Teacher = RoleRank.Teacher - RoleHierarchy.StaffRolesBase,
        Secretary = RoleRank.Secretary - RoleHierarchy.StaffRolesBase,
        SchoolAdmin = RoleRank.SchoolAdmin - RoleHierarchy.StaffRolesBase,
        SchoolOwner = RoleRank.SchoolOwner - RoleHierarchy.StaffRolesBase
    }
}
