using Newtonsoft.Json;
using WordPressPCL.Models;

namespace Phoenix.WordPress.Puller.Models
{
    internal class AcfUser : User
    {
        [JsonProperty("acf")]
        public UserAcf Acf { get; set; }
    }

    public class UserAcf
    {
        [JsonProperty("role")]
        public int? Role { get; set; }
    }
}
