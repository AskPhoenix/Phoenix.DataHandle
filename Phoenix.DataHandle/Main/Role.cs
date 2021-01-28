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
        Student = 10,
        Parent,
        
        // School-side
        Teacher = 20,
        Secretary,
        SchoolAdmin,
        SchoolOwner,

        // Backend-side
        SchoolTester = 30,
        SuperTester,
        SuperAdmin
    }

    //TODO: Locale
    public static class RoleExtensions
    {
        public static IEnumerable<Role> GetAll()
        {
            return Enum.GetValues(typeof(Role)).Cast<Role>();
        }

        public static string ToString(this Role me)
        {
            return me.ToString();
        }

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