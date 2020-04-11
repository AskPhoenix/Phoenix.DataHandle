using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Models
{
    public partial class School
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string addressLine { get; set; }
        public string info { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
