using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class User
    {
        public int AspNetUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool TermsAccepted { get; set; }

        public virtual AspNetUsers AspNetUser { get; set; }
    }
}
