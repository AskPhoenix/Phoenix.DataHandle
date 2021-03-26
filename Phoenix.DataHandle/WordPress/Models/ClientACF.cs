using Newtonsoft.Json;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.WordPress.Models.Uniques;
using Phoenix.DataHandle.WordPress.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.WordPress.Models
{
    public class ClientACF : IModelACF<AspNetUsers>
    {
        [JsonProperty(PropertyName = "student_full_name")]
        public string StudentFullName { get; set; }

        [JsonProperty(PropertyName = "course_codes")]
        public string CourseCodesString { get; set; }

        [JsonProperty(PropertyName = "needs_parent_authorization")]
        public string NeedsParentAuthorizationString { get; set; }

        [JsonProperty(PropertyName = "student_phone")]
        public long? StudentPhoneNumber { get; set; }

        [JsonProperty(PropertyName = "parent1_full_name")]
        public string Parent1FullName { get; set; }

        [JsonProperty(PropertyName = "parent1_phone")]
        public long? Parent1PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "parent2_full_name")]
        public string Parent2FullName { get; set; }

        [JsonProperty(PropertyName = "parent2_phone")]
        public long? Parent2PhoneNumber { get; set; }

        public bool IsSelfDetermined => string.Compare(this.NeedsParentAuthorizationString, "No", StringComparison.InvariantCultureIgnoreCase) == 0;
        public long? AffiliatedPhoneNumber => this.Parent1PhoneNumber ?? this.Parent2PhoneNumber;

        public Expression<Func<AspNetUsers, bool>> MatchesUnique => u =>
            u.PhoneNumber == StudentPhoneNumber.ToString() ||
            u.AffiliatedPhoneNumber == Parent1PhoneNumber.ToString() ||
            u.AffiliatedPhoneNumber == Parent2PhoneNumber.ToString();

        public SchoolUnique SchoolUnique { get; set; }

        [JsonConstructor]
        public ClientACF(long? studentPhoneNumber, long? parent1PhoneNumber, long? parent2PhoneNumber)
        {
            if (studentPhoneNumber is null || parent1PhoneNumber is null || parent2PhoneNumber is null)
                throw new ArgumentNullException("", "At least one parameter should be a non-null value");

            this.StudentPhoneNumber = studentPhoneNumber;
            this.Parent1PhoneNumber = parent1PhoneNumber;
            this.Parent1PhoneNumber = parent2PhoneNumber;
        }

        public ClientACF(ClientACF other)
        {
            this.StudentFullName = other.StudentFullName;
            this.CourseCodesString = other.CourseCodesString;
            this.NeedsParentAuthorizationString = other.NeedsParentAuthorizationString;
            this.StudentPhoneNumber = other.StudentPhoneNumber;
            this.Parent1FullName = other.Parent1FullName;
            this.Parent1PhoneNumber = other.Parent1PhoneNumber;
            this.Parent2FullName = other.Parent2FullName;
            this.Parent2PhoneNumber = other.Parent2PhoneNumber;
            this.SchoolUnique = other.SchoolUnique;
        }

        public AspNetUsers ToContext()
        {
            var user = new AspNetUsers
            {
                PhoneNumber = this.StudentPhoneNumber.HasValue ? this.StudentPhoneNumber.ToString() : null,
                AffiliatedPhoneNumber = this.AffiliatedPhoneNumber.HasValue ? this.AffiliatedPhoneNumber.ToString() : null,
                CreatedApplicationType = ApplicationType.Scheduler
            };

            //TODO: Assign a better UserName
            user.UserName = this.StudentFullName.Substring(0, 3) + (user.PhoneNumber ?? user.AffiliatedPhoneNumber);
            user.NormalizedUserName = user.UserName.ToUpperInvariant();

            return user;
        }

        public IModelACF<AspNetUsers> WithTitleCase()
        {
            return new ClientACF(this)
            {
                StudentFullName = this.StudentFullName.ToTitleCase(),
                Parent1FullName = this.Parent1FullName.ToTitleCase(),
                Parent2FullName = this.Parent2FullName.ToTitleCase()
            };
        }

        public User ExtractUser()
        {
            return new User()
            {
                FirstName = UserInfoHelper.GetFirstName(this.StudentFullName).Truncate(255),
                LastName = UserInfoHelper.GetFirstName(this.StudentFullName).Truncate(255),
                IsSelfDetermined = this.IsSelfDetermined
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

        public List<AspNetUsers> ExtractParents()
        {
            //TODO: Assign a better UserName

            var parents = new List<AspNetUsers>(2);
            parents.Add(new AspNetUsers
            {
                PhoneNumber = this.Parent1PhoneNumber.ToString(),
                AffiliatedPhoneNumber = null,
                CreatedApplicationType = ApplicationType.Scheduler,
                UserName = this.Parent1FullName.Substring(0, 3) + this.Parent1PhoneNumber,
                NormalizedUserName = this.Parent1FullName.Substring(0, 3).ToUpperInvariant() + this.Parent1PhoneNumber
            });

            if (!string.IsNullOrEmpty(this.Parent2FullName) && this.Parent2PhoneNumber.HasValue)
                parents.Add(new AspNetUsers
                {
                    PhoneNumber = this.Parent2PhoneNumber.ToString(),
                    AffiliatedPhoneNumber = null,
                    CreatedApplicationType = ApplicationType.Scheduler,
                    UserName = this.Parent2FullName.Substring(0, 3) + this.Parent2PhoneNumber,
                    NormalizedUserName = this.Parent2FullName.Substring(0, 3).ToUpperInvariant() + this.Parent2PhoneNumber
                });

            return parents;
        }

        public List<User> ExtractParentUsers()
        {
            var parentUsers = new List<User>(2);
            parentUsers.Add(new User
            {
                FirstName = UserInfoHelper.GetFirstName(this.Parent1FullName).Truncate(255),
                LastName = UserInfoHelper.GetLastName(this.Parent1FullName).Truncate(255),
                IsSelfDetermined = true
            });

            if (!string.IsNullOrEmpty(this.Parent2FullName) && this.Parent2PhoneNumber.HasValue)
                parentUsers.Add(new User
                {
                    FirstName = UserInfoHelper.GetFirstName(this.Parent2FullName).Truncate(255),
                    LastName = UserInfoHelper.GetLastName(this.Parent2FullName).Truncate(255),
                    IsSelfDetermined = true
                });

            return parentUsers;
        }
    }
}
