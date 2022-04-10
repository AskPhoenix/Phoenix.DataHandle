using Phoenix.Language.Types;
using System;
using System.Linq;

namespace Phoenix.DataHandle.Main.Types
{
    public enum Role
    {
        // Inactive roles
        None = 0,
        
        // Client roles
        Student = RoleHierarchy.ClientRolesBase,
        Parent,
        
        // Staff roles
        Teacher = RoleHierarchy.StaffRolesBase,
        Secretary,
        SchoolAdmin,
        SchoolOwner,

        // Backend roles
        SchoolTester = RoleHierarchy.BackendRolesBase,  // School backend role
        SuperTester = RoleHierarchy.SuperRolesBase,     // Super backend roles
        SuperAdmin
    }

    public static class RoleHierarchy
    {
        public const int ClientRolesBase = 10;
        public const int StaffRolesBase = 20;
        public const int BackendRolesBase = 30;
        public const int SuperRolesBase = 35;
    }

    public static class RoleExtensions
    {
        public static bool IsClient(this Role me) => 
            (int)me >= RoleHierarchy.ClientRolesBase && (int)me < RoleHierarchy.StaffRolesBase;
        public static bool IsStaff(this Role me) => 
            (int)me >= RoleHierarchy.StaffRolesBase && (int)me < RoleHierarchy.BackendRolesBase;
        public static bool IsBackend(this Role me) => (int)me >= RoleHierarchy.BackendRolesBase;
        public static bool IsSuper(this Role me) => (int)me >= RoleHierarchy.SuperRolesBase;
        public static bool IsSchoolBackend(this Role me) => me.IsBackend() && !me.IsSuper();
        public static bool IsStaffHead(this Role me) => me.IsStaff() && me >= Role.SchoolAdmin;
        public static bool IsStaffOrBackend(this Role me) => me.IsStaff() || me.IsBackend();
        public static bool IsActive(this Role me) => me != Role.None;

        public static Role[] GetAllRoles() => Enum.GetValues<Role>();
        public static Role[] GetClientRoles() => GetAllRoles().Where(r => r.IsClient()).ToArray();
        public static Role[] GetStaffRoles() => GetAllRoles().Where(r => r.IsStaff()).ToArray();
        public static Role[] GetBackendRoles() => GetAllRoles().Where(r => r.IsBackend()).ToArray();
        public static Role[] GetSuperRoles() => GetAllRoles().Where(r => r.IsSuper()).ToArray();
        public static Role[] GetSchoolBackendRoles() => GetAllRoles().Where(r => r.IsSchoolBackend()).ToArray();
        public static Role[] GetStaffHeadRoles() => GetAllRoles().Where(r => r.IsStaffHead()).ToArray();
        public static Role[] GetStaffAndBackendRoles() => GetAllRoles().Where(r =>r.IsStaffOrBackend()).ToArray();
        public static Role[] GetActiveRoles() => GetAllRoles().Where(r => r.IsActive()).ToArray();
        
        public static string ToFriendlyString(this Role me)
        {
            return me switch
            {
                Role.Student        => RoleResources.Student,
                Role.Parent         => RoleResources.Parent,
                Role.Teacher        => RoleResources.Teacher,
                Role.Secretary      => RoleResources.Secretary,
                Role.SchoolAdmin    => RoleResources.SchoolAdmin,
                Role.SchoolOwner    => RoleResources.SchoolOwner,
                Role.SchoolTester   => RoleResources.SchoolTester,
                Role.SuperTester    => RoleResources.SuperTester,
                Role.SuperAdmin     => RoleResources.SuperAdmin,
                _                   => string.Empty
            };
        }

        public static Role ToRole(this string me)
        {
            return GetAllRoles().SingleOrDefault(r => string.Equals(r.ToString(), me, StringComparison.OrdinalIgnoreCase));
        }

        public static bool TryToRole(this string me, out Role role)
        {
            try
            {
                role = me.ToRole();
                return true;
            }
            catch(InvalidOperationException)
            {
                role = Role.None;
            }

            return false;
        }
    }
}
