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
        Scheduler = 2
    }

    public static class ApplicationTypeExtensions
    {
        public static IEnumerable<ApplicationType> GetAll()
        {
            return Enum.GetValues(typeof(ApplicationType)).Cast<ApplicationType>();
        }

        public static string ToFriendlyString(this ApplicationType me)
        {
            return me switch
            {
                ApplicationType.Undefined => "Undefined",
                ApplicationType.None => "None",
                ApplicationType.Pwa => "Pwa",
                ApplicationType.Scheduler => "Scheduler",
                _ => string.Empty,
            };
        }
    }
}
