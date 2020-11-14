using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using System;
using System.Linq;

namespace Phoenix.DataHandle.WordPress.ACF
{
    class SchoolUser
    {
        [JsonProperty(PropertyName = "code")]
        public short Code { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string RoleString { get; set; }

        [JsonProperty(PropertyName = "second_role")]
        public string SecondRoleString { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public long Phone { get; set; }

        [JsonProperty(PropertyName = "course_codes")]
        public string CourseCodesString { get; set; }

        public int SchoolId { get; set; }

        public bool MatchesUnique(UserSchool ctxUserSchool)
        {
            return ctxUserSchool != null
                && ctxUserSchool.SchoolId == this.SchoolId
                && ctxUserSchool.Code == this.Code;
        }

        public AspNetUsers ExtractAspNetUser()
        {
            string username = $"{this.FirstName?.First()}{this.LastName}{this.SchoolId}{this.Code}".ToLower();

            return new AspNetUsers()
            {
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                PhoneNumber = this.Phone.ToString().Substring(0, Math.Min(this.Phone.ToString().Length, 50)),
                CreatedApplicationType = 0,
                CreatedAt = DateTimeOffset.Now
            };
        }

        public AspNetUsers ExtractAspNetUser(AspNetUsers aspNetUser)
        {
            aspNetUser.UserName = $"{this.FirstName?.First()}{this.LastName}{this.SchoolId}{this.Code}".ToLower();
            aspNetUser.NormalizedUserName = aspNetUser.UserName.ToUpper();
            aspNetUser.PhoneNumber = this.ExtractAspNetUser().PhoneNumber;
            return aspNetUser;
        }

        public User ExtractUser(int aspNetUserId)
        {
            return new User()
            {
                AspNetUserId = aspNetUserId,
                FirstName = this.FirstName?.Substring(0, Math.Min(this.FirstName.Length, 255)),
                LastName = this.LastName?.Substring(0, Math.Min(this.LastName.Length, 255))
            };
        }

        public User ExtractUser(User user)
        {
            var extrUser = this.ExtractUser(0);
            user.FirstName = extrUser.FirstName;
            user.LastName = extrUser.LastName;
            return user;
        }

        public UserSchool ExtractUserSchool(int aspNetUserId)
        {
            return new UserSchool()
            {
                AspNetUserId = aspNetUserId,
                Code = this.Code,
                SchoolId = this.SchoolId,
                EnrolledOn = DateTimeOffset.Now,
                CreatedAt = DateTimeOffset.Now
            };
        }

        public short[] ExtractCourseCodes()
        {
            if (string.IsNullOrEmpty(this.CourseCodesString))
                return new short[0];

            return this.CourseCodesString.Split(',').Select(sc => short.Parse(sc.Trim())).ToArray();
        }

        public SchoolUser WithTitleCaseText()
        {
            return new SchoolUser()
            {
                Code = this.Code,
                FirstName = this.FirstName?.UpperToTitleCase(),
                LastName = this.LastName?.UpperToTitleCase(),
                RoleString = this.RoleString,
                SecondRoleString = this.SecondRoleString,
                Phone = this.Phone,
                CourseCodesString = this.CourseCodesString,
                SchoolId = this.SchoolId
            };
        }
    }
}
