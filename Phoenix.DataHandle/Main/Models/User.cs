using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class User
    {
        public int AspNetUserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool TermsAccepted { get; set; }
        public bool IsSelfDetermined { get; set; }
        public string? IdentifierCode { get; set; }
        public DateTimeOffset? IdentifierCodeCreatedAt { get; set; }

        public virtual AspNetUser AspNetUser { get; set; } = null!;
    }
}
