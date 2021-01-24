using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Parenthood
    {
        public int ParentId { get; set; }
        public int ChildId { get; set; }

        public virtual AspNetUsers Child { get; set; }
        public virtual AspNetUsers Parent { get; set; }
    }
}
