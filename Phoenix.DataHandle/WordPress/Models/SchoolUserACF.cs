using Newtonsoft.Json;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.WordPress.Models
{
    public class SchoolUserACF : IModelACF<AspNetUsers>
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
        public Role RoleType => this.RoleString.ToRole();
        public Role SecondRoleType => this.SecondRoleString.ToRole();

        private string GetUsername()
        {
            return $"{this.FirstName?.First()}{this.LastName}{this.SchoolId}{this.Code}".ToLowerInvariant();
        }

        public Expression<Func<AspNetUsers, bool>> MatchesUnique => 
            u => u != null && u.UserSchool.Any(us => us.SchoolId == this.SchoolId && us.Code == Code);

        public SchoolUserACF() { }
        public SchoolUserACF(int schoolId, short code)
        {
            this.SchoolId = schoolId;
            this.Code = code;
        }

        public AspNetUsers ToContext()
        {
            return new AspNetUsers()
            {
                UserName = GetUsername(),
                NormalizedUserName = GetUsername().ToUpperInvariant(),
                PhoneNumber = this.Phone.ToString().Substring(0, Math.Min(this.Phone.ToString().Length, 50)),
                CreatedApplicationType = ApplicationType.Scheduler,
                CreatedAt = DateTimeOffset.Now
            };
        }

        public IModelACF<AspNetUsers> WithTitleCase()
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

        public UserSchool ExtractUserSchool()
        {
            return new UserSchool()
            {
                Code = this.Code,
                EnrolledOn = DateTimeOffset.Now
            };
        }

        public User ExtractUser()
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
