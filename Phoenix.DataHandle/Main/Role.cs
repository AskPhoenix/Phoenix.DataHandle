using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DataHandle.Main
{
    public enum Role
    {
        Undefined = 0,
        None,
        
        // Client-side
        Student = RoleAttributes.ClientRolesBase,
        Parent,
        
        // School-side
        Teacher = RoleAttributes.StaffRolesBase,
        Secretary,
        SchoolAdmin,
        SchoolOwner,

        // Backend-side
        SchoolTester = RoleAttributes.BackendRolesBase,
        SuperTester = RoleAttributes.SuperRolesBase,
        SuperAdmin
    }

    public static class RoleAttributes
    {
        public const int ClientRolesBase = 10;
        public const int StaffRolesBase = 20;
        public const int BackendRolesBase = 30;
        public const int SuperRolesBase = 35;
    }

    //TODO: Locale
    public static class RoleExtensions
    {
        public static bool IsClient(this Role role) => (int)role >= RoleAttributes.ClientRolesBase && (int)role < RoleAttributes.StaffRolesBase;
        public static bool IsStaff(this Role role) => (int)role >= RoleAttributes.StaffRolesBase && (int)role < RoleAttributes.BackendRolesBase;
        public static bool IsBackend(this Role role) => (int)role >= RoleAttributes.BackendRolesBase;
        public static bool IsSuper(this Role role) => (int)role >= RoleAttributes.SuperRolesBase;

        //TODO: Is this needed?
        public static bool IsStaffAdmin(this Role role) => role.IsStaff() && role >= Role.SchoolAdmin;
        

        public static IEnumerable<Role> GetClientRoles() => GetAll().Where(r => r.IsClient());

        public static IEnumerable<Role> GetStaffRoles() => GetAll().Where(r => r.IsStaff());

        public static IEnumerable<Role> GetBackendRoles() => GetAll().Where(r => r.IsBackend());

        public static IEnumerable<Role> GetSuperRoles() => GetAll().Where(r => r.IsSuper());

        public static IEnumerable<Role> GetSchoolBackendRoles() => GetAll().Where(r => r.IsBackend() && !r.IsSuper());

        public static IEnumerable<Role> GetStaffAdminRoles() => GetAll().Where(r => r.IsStaffAdmin());

        public static IEnumerable<Role> GetAll() => Enum.GetValues(typeof(Role)).Cast<Role>();

        public static string ToString(this Role me)
        {
            return me.ToString();
        }

        //TODO: Locale
        public static string ToFriendlyString(this Role me)
        {
            return me switch
            {
                Role.Undefined      => "Undefined",
                Role.None           => "None",
                Role.Student        => "Student",
                Role.Parent         => "Parent",
                Role.Teacher        => "Teacher",
                Role.Secretary      => "Secretary",
                Role.SchoolAdmin    => "Admin",
                Role.SchoolOwner    => "School Owner",
                Role.SchoolTester   => "School Tester",
                Role.SuperTester    => "Super Tester",
                Role.SuperAdmin     => "Super Admin",
                _                   => string.Empty
            };
        }

        //TODO: Change Normalized Role Names
        public static string ToNormalizedString(this Role me)
        {
            return me switch
            {
                Role.Undefined      => "Απροσδιόριστος",
                Role.None           => "Κανένας",
                Role.Student        => "Μαθητής",
                Role.Parent         => "Γονέας / Κηδεμόνας",
                Role.Teacher        => "Εκπαιδευτικός",
                Role.Secretary      => "Γραμματέας",
                Role.SchoolAdmin    => "Διαχειριστής",
                Role.SchoolOwner    => "Ιδιοκτήτης",
                Role.SchoolTester   => "Δοκιμαστής Σχολείου",
                Role.SuperTester    => "Υπερδοκιμαστής",
                Role.SuperAdmin     => "Υπερδιαχειριστής",
                _                   => string.Empty
            };
        }

        public static Role ToRole(this string me)
        {
            try
            {
                return GetAll().SingleOrDefault(a => string.Equals(a.ToString(), me, StringComparison.OrdinalIgnoreCase));
            }
            catch(InvalidOperationException)
            {
                return Role.Undefined;
            }
        }

        public static Role ToRoleFromNormalized(this string me)
        {
            try
            {
                return GetAll().SingleOrDefault(a => string.Equals(a.ToNormalizedString(), me, StringComparison.OrdinalIgnoreCase));
            }
            catch (InvalidOperationException)
            {
                return Role.Undefined;
            }
        }
    }
}