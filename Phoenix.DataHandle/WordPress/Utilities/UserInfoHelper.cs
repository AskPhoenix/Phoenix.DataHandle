using System;
using System.Linq;

namespace Phoenix.DataHandle.WordPress.Utilities
{
    public static class UserInfoHelper
    {
        public static string GetFirstName(string fullname)
        {
            if (string.IsNullOrEmpty(fullname))
                throw new ArgumentNullException(nameof(fullname));

            var names = fullname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Join(' ', names.Take((int)Math.Ceiling(names.Length / 2.0)));
        }

        public static string GetLastName(string fullname)
        {
            if (string.IsNullOrEmpty(fullname))
                throw new ArgumentNullException(nameof(fullname));

            var names = fullname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (names.Length == 1)
                return string.Empty;

            return string.Join(' ', names.TakeLast(names.Length / 2));
        }
    }
}
