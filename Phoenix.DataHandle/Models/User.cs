using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Models
{
    public partial class User
    {
        public int AspNetUserId { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }

        public virtual AspNetUsers AspNetUser { get; set; }
    }
}
