﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class SchoolSettings
    {
        public int SchoolId { get; set; }
        public string Language { get; set; }
        public string Locale2 { get; set; }
        public string TimeZone { get; set; }

        public virtual School School { get; set; }
    }
}
