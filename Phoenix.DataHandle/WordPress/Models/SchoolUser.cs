using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Relationships;
using Phoenix.DataHandle.Utilities;
using System;
using System.Linq;

namespace Phoenix.DataHandle.WordPress.Models
{
    public class SchoolUserACF : IModelACF<IUserSchool>
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
        public int UserId { get; set; }

        private string GetUsername()
        {
            return $"{this.FirstName?.First()}{this.LastName}{this.SchoolId}{this.Code}".ToLower();
        }

        public bool MatchesUnique(IUserSchool ctxUserSchool)
        {
            return ctxUserSchool != null
                && (ctxUserSchool as UserSchool).SchoolId == this.SchoolId
                && ctxUserSchool.Code == this.Code;
        }

        public IUserSchool ToContext()
        {
            return new UserSchool()
            {
                Code = this.Code,
                EnrolledOn = DateTimeOffset.Now,
                CreatedAt = DateTimeOffset.Now
            };
        }

        public IModelACF<IUserSchool> WithTitleCase()
        {
            return new SchoolUserACF()
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

        public IAspNetUsers ExtractAspNetUser()
        {
            return new AspNetUsers()
            {
                UserName = GetUsername(),
                NormalizedUserName = GetUsername().ToUpper(),
                PhoneNumber = this.Phone.ToString().Substring(0, Math.Min(this.Phone.ToString().Length, 50)),
                CreatedApplicationType = Main.ApplicationType.Scheduler,
                CreatedAt = DateTimeOffset.Now
            };
        }

        //public IAspNetUsers UpdateAspNetUser(IAspNetUsers aspNetUser)
        //{
        //    aspNetUser.UserName = GetUsername();
        //    aspNetUser.NormalizedUserName = aspNetUser.UserName.ToUpper();
        //    aspNetUser.PhoneNumber = this.Phone.ToString().Substring(0, Math.Min(this.Phone.ToString().Length, 50));

        //    return aspNetUser;
        //}

        public IUser ExtractUser()
        {
            return new User()
            {
                FirstName = this.FirstName?.Substring(0, Math.Min(this.FirstName.Length, 255)),
                LastName = this.LastName?.Substring(0, Math.Min(this.LastName.Length, 255))
            };
        }

        public short[] ExtractCourseCodes()
        {
            if (string.IsNullOrEmpty(this.CourseCodesString))
                return new short[0];

            return this.CourseCodesString.Split(',').Select(sc => short.Parse(sc.Trim())).ToArray();
        }
    }
}
