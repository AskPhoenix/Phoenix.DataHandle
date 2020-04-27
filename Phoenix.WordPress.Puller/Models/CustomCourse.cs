using Newtonsoft.Json;
using System.Collections.Generic;
using WordPressPCL.Models;

namespace Phoenix.WordPress.Puller.Models
{
    internal class CustomCourse : Post
    {
        [JsonProperty("acf")]
        public UserAcf Acf { get; set; }
    }

    internal class CourseAcf
    {
        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("books")]
        public List<int> BookWpIds { get; set; }
    }
}
