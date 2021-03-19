using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class User
    {
        public int AspNetUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool TermsAccepted { get; set; }
        public bool IsSelfDetermined { get; set; }
        public string IdentifierCode { get; set; }
        public DateTimeOffset? IdentifierCodeCreatedAt { get; set; }

        public virtual AspNetUsers AspNetUser { get; set; }
    }
}
