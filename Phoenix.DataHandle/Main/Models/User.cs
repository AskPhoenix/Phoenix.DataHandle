using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class User
    {
        public int AspNetUserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool TermsAccepted { get; set; }
        public bool IsSelfDetermined { get; set; }
        public string? IdentifierCode { get; set; }
        public DateTime? IdentifierCodeCreatedAt { get; set; }

        public virtual AspNetUser AspNetUser { get; set; } = null!;
    }
}
