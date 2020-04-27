using Newtonsoft.Json;
using System.Collections.Generic;
using WordPressPCL.Models;

namespace Phoenix.WordPress.Puller.Models
{
    internal class CustomUser : User
    {
        [JsonProperty("acf")]
        public UserAcf Acf { get; set; }
    }

    internal class UserAcf
    {
        [JsonProperty("role")]
        public int Role { get; set; }

        [JsonProperty("courses")]
        public List<int> CourseWpIds { get; set; }
    }
}
