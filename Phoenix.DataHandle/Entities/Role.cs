using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DataHandle.Entities
{
    public enum Role
    {
        Undefined = -1,
        None = 0,
        SuperAdmin = 1,
        Admin = 2,
        SchoolOwner = 3,
        Teacher = 4,
        Parent = 5,
        Student = 6,
    }

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

        public static string ToStringNormalize(this Role me)
        {
            return me.ToString().ToUpper();
        }

        public static Role ToRole(this string me)
        {
            try
            {
                return GetAll().SingleOrDefault(a => string.Equals(a.ToString(), me, StringComparison.CurrentCultureIgnoreCase));
            }
            catch
            {
                return Role.Undefined;
            }
        }

    }

}