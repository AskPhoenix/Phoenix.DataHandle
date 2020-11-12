using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DataHandle.Main
{
    public enum ApplicationType
    {
        Undefined = -1,
        None = 0,
        Pwa = 1,
    }

    public static class ApplicationTypeExtensions
    {
        public static IEnumerable<ApplicationType> getAll()
        {
            return Enum.GetValues(typeof(ApplicationType)).Cast<ApplicationType>();
        }

        public static string toFriendlyString(this ApplicationType me)
        {
            switch (me)
            {
                case ApplicationType.Undefined:
                    return "Undefined";
                case ApplicationType.None:
                    return "None";
                case ApplicationType.Pwa:
                    return "Pwa";
                default:
                    return string.Empty;
            }

        }
    }
}
