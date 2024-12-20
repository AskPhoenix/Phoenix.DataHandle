﻿using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Base
{
    public static class CourseExtensions
    {
        public static string GetNameWithSubcourse(this ICourseBase me)
        {
            if (me.SubCourse is null)
                return me.Name;

            return me.Name + " - " + me.SubCourse;
        }
    }

    public static class UserExtensions
    {
        private static string ResolveName(this IUserBase me, bool selFirstName)
        {
            var names = me.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (!selFirstName && names.Length == 1)
                return string.Empty;

            string tore;
            if (selFirstName)
                tore = string.Join(' ', names.Take((int)Math.Ceiling(names.Length / 2.0)));
            else
                tore = string.Join(' ', names.TakeLast(names.Length / 2));

            return tore;
        }

        public static string ResolveFirstName(this IUserBase me)
        {
            return me.ResolveName(selFirstName: true);
        }

        public static string ResolveLastName(this IUserBase me)
        {
            return me.ResolveName(selFirstName: false);
        }

        public static string BuildFullName(this IUserBase me)
        {
            return me.FirstName + " " + me.LastName;
        }

        public static string GenerateUserName(this IUserBase me,
            IEnumerable<int> schoolCodes, string linkedPhone)
        {
            return GenerateUserName(schoolCodes, linkedPhone, me.DependenceOrder);
        }

        public static string GenerateUserName(
            IEnumerable<int> schoolCodes, string linkedPhone, int dependenceOrder)
        {
            if (string.IsNullOrWhiteSpace(linkedPhone))
                throw new ArgumentNullException(nameof(linkedPhone));

            return $"S{string.Join('_', schoolCodes)}__P{linkedPhone}__O{dependenceOrder}";
        }
    }
}
