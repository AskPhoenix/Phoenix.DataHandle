using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DataHandle.Main
{
    public enum Role
    {
        Undefined = -1,
        None,
        Student,
        Parent,
        Secretary,
        Teacher,
        Admin,
        SchoolOwner,
        SuperAdmin,
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
                Role.Secretary      => "Secretary",
                Role.Teacher        => "Teacher",
                Role.Admin          => "Admin",
                Role.SchoolOwner    => "SchoolOwner",
                Role.SuperAdmin     => "SuperAdmin",
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
                Role.Secretary      => "Γραμματέας",
                Role.Teacher        => "Εκπαιδευτικός",
                Role.Admin          => "Διαχειριστής",
                Role.SchoolOwner    => "Ιδιοκτήτης",
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