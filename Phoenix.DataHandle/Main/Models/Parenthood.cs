using System;
using System.Collections.Generic;

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
