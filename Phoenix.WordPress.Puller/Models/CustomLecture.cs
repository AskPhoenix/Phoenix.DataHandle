using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WordPressPCL.Models;

namespace Phoenix.WordPress.Puller.Models
{
    internal class CustomLecture : Post
    {
        [JsonProperty("acf")]
        public UserAcf Acf { get; set; }
    }

    internal class LectureAcf
    {
        [JsonProperty("course")]
        public int CourseWpId { get; set; }

        [JsonProperty("teachers")]
        public List<int> TeacherWpIds { get; set; }

        [JsonProperty("classroom")]
        public int ClassroomWpId { get; set; }

        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }
    }
}
