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

    public static class PersonnelRoleRankApiExtensions
    {
        public static RoleRank ConvertToRoleRank(PersonnelRoleRankApi personnelRoleRank)
        {
            var roleRank = (RoleRank)(personnelRoleRank + RoleHierarchy.StaffRolesBase);

            if (!roleRank.IsStaff())
                return RoleRank.None;

            return roleRank;
        }
    }
}
