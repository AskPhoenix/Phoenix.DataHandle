﻿using System;
using System.Linq;

namespace Phoenix.DataHandle.Main.Entities
{
    public static class CourseExtensions
    {
        public static string GetNameWithSubcourse(this ICourse me)
        {
            if (me.SubCourse is null)
                return me.Name;
            
            return me.Name + " - " + me.SubCourse;
        }
    }

    public static class UserInfoExtensions
    {
        private static string ResolveName(this IUserInfo me, bool selFirstName)
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

        public static string ResolveFirstName(this IUserInfo me)
        {
            return me.ResolveName(selFirstName: true);
        }

        public static string ResolveLastName(this IUserInfo me)
        {
            return me.ResolveName(selFirstName: false);
        }

        public static string BuildFullName(this IUserInfo me)
        {
            return me.FirstName + " " + me.LastName;
        }
    }

    public static class UserExtensions
    {
        public static string GetUserName(this IUserInfo me, int schoolCode, string linkedPhone)
        {
            if (string.IsNullOrWhiteSpace(linkedPhone))
                throw new ArgumentNullException(nameof(linkedPhone));

            return $"S{schoolCode}_P{linkedPhone}_N{me.FirstName}_O{me.DependenceOrder}";
        }
    }
}
