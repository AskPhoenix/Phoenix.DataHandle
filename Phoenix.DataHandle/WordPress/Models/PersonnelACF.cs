using Newtonsoft.Json;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.WordPress.Models.Uniques;
using Phoenix.DataHandle.WordPress.Utilities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.WordPress.Models
{
    public class PersonnelACF : IModelACF<AspNetUsers>
    {
        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string RoleString { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public long PhoneNumber { get; }

        [JsonProperty(PropertyName = "course_codes")]
        public string CourseCodesString { get; set; }

        public string PhoneString => this.PhoneNumber.ToString();
        public Role RoleType => this.RoleString.Replace(" ", "").ToRole();
        public string FirstName => UserInfoHelper.GetFirstName(this.FullName);
        public string LastName => UserInfoHelper.GetLastName(this.FullName);
        public static string GetUserName(User user, int schoolId, string phone)
        {
            return $"{user.FirstName.Substring(0, 4)}_{user.LastName}_{schoolId}_{phone}".ToLowerInvariant();
        }

        public Expression<Func<AspNetUsers, bool>> MatchesUnique => u => u.PhoneNumber == this.PhoneString;

        public SchoolUnique SchoolUnique { get; set; }

        [JsonConstructor]
        public PersonnelACF(long phoneNumber) 
        {
            this.PhoneNumber = phoneNumber;
        }

        public PersonnelACF(PersonnelACF other)
        {
            this.FullName = other.FullName;
            this.RoleString = other.RoleString;
            this.PhoneNumber = other.PhoneNumber;
            this.CourseCodesString = other.CourseCodesString;
        }

        public AspNetUsers ToContext()
        {
            return new AspNetUsers
            {
                PhoneNumber = this.PhoneString,
                CreatedApplicationType = ApplicationType.Scheduler
            };
        }

        public IModelACF<AspNetUsers> WithTitleCase()
        {
            return new PersonnelACF(this)
            {
                FullName = this.FullName.ToTitleCase()
            };
        }

        public User ExtractUser()
        {
            return new User()
            {
                FirstName = this.FirstName.Truncate(255),
                LastName = this.LastName.Truncate(255),
                IsSelfDetermined = true
            };
        }

        public short[] ExtractCourseCodes()
        {
            return this.CourseCodesString?.
                Split(',', StringSplitOptions.RemoveEmptyEntries).
                Select(cc => short.Parse(cc.Trim())).
                ToArray()
                ?? Array.Empty<short>();
        }
    }
}
