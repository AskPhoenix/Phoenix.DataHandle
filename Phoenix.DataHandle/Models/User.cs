using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Models
{
    public partial class User
    {
        public int aspNetUserId { get; set; }
        public string surname { get; set; }
        public string forename { get; set; }

        public virtual AspNetUsers aspNetUser { get; set; }
    }
}
