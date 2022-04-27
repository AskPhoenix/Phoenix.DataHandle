using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class SchoolSetting
    {
        public int SchoolId { get; set; }
        public string Country { get; set; } = null!;
        public string PrimaryLanguage { get; set; } = null!;
        public string PrimaryLocale { get; set; } = null!;
        public string SecondaryLanguage { get; set; } = null!;
        public string SecondaryLocale { get; set; } = null!;
        public string TimeZone { get; set; } = null!;
        public string PhoneCountryCode { get; set; } = null!;

        public virtual School School { get; set; } = null!;
    }
}
