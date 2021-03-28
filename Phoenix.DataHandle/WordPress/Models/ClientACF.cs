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
        public long? StudentPhoneNumber 
        { 
            get => this.IsSelfDetermined ? studentPhoneNumber : null;
            private set => this.studentPhoneNumber = value;
        }
        private long? studentPhoneNumber;

        [JsonProperty(PropertyName = "parent1_full_name")]
        public string Parent1FullName { get => parent1FullName; set => parent1FullName = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string parent1FullName;

        [JsonProperty(PropertyName = "parent1_phone")]
        public long? Parent1PhoneNumber { get; }

        [JsonProperty(PropertyName = "parent2_full_name")]
        public string Parent2FullName { get => parent2FullName; set => parent2FullName = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string parent2FullName;

        [JsonProperty(PropertyName = "parent2_phone")]
        public long? Parent2PhoneNumber { get; }

        public bool IsSelfDetermined => string.Compare(this.NeedsParentAuthorizationString, "No", StringComparison.InvariantCultureIgnoreCase) == 0;
        public string TopPhoneNumber => (this.StudentPhoneNumber ?? this.Parent1PhoneNumber ?? this.Parent2PhoneNumber).ToString();

        public string StudentPhoneString => this.StudentPhoneNumber.HasValue ? this.StudentPhoneNumber.ToString() : null;
        public string Parent1PhoneString => this.Parent1PhoneNumber.HasValue ? this.Parent1PhoneNumber.ToString() : null;
        public string Parent2PhoneString => this.Parent2PhoneNumber.HasValue ? this.Parent2PhoneNumber.ToString() : null;

        public bool HasParent1 => !string.IsNullOrEmpty(this.Parent1FullName) && this.Parent1PhoneNumber.HasValue;
        public bool HasParent2 => !string.IsNullOrEmpty(this.Parent2FullName) && this.Parent2PhoneNumber.HasValue;

        public string StudentFirstName => UserInfoHelper.GetFirstName(this.StudentFullName);
        public string StudentLastName => UserInfoHelper.GetLastName(this.StudentFullName);

        public static string GetUserName(User user, int schoolId, string phone)
        {
            return $"{user.FirstName.Substring(0, 4)}_{user.LastName}_{schoolId}_{phone}".ToLowerInvariant();
        }

        public Expression<Func<AspNetUsers, bool>> MatchesUnique => u => this.IsSelfDetermined && u.PhoneNumber == StudentPhoneString;

        public SchoolUnique SchoolUnique { get; set; }

        [JsonConstructor]
        public ClientACF(long? studentPhoneNumber, long? parent1PhoneNumber, long? parent2PhoneNumber)
        {
            this.StudentPhoneNumber = studentPhoneNumber;
            this.Parent1PhoneNumber = parent1PhoneNumber;
            this.Parent2PhoneNumber = parent2PhoneNumber;
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
                CreatedApplicationType = ApplicationType.Scheduler
            };

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
                FirstName = this.StudentFirstName.Truncate(255),
                LastName =this.StudentLastName.Truncate(255),
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
            var parents = new List<AspNetUsers>(2);

            if (this.HasParent1)
                parents.Add(new AspNetUsers
                {
                    PhoneNumber = this.Parent1PhoneNumber.ToString(),
                    CreatedApplicationType = ApplicationType.Scheduler,
                    UserName = this.Parent1FullName.Substring(0, 3) + this.Parent1PhoneNumber,
                    NormalizedUserName = this.Parent1FullName.Substring(0, 3).ToUpperInvariant() + this.Parent1PhoneNumber
                });

            if (this.HasParent2)
                parents.Add(new AspNetUsers
                {
                    PhoneNumber = this.Parent2PhoneNumber.ToString(),
                    CreatedApplicationType = ApplicationType.Scheduler,
                    UserName = this.Parent2FullName.Substring(0, 3) + this.Parent2PhoneNumber,
                    NormalizedUserName = this.Parent2FullName.Substring(0, 3).ToUpperInvariant() + this.Parent2PhoneNumber
                });

            return parents;
        }

        public List<User> ExtractParentUsers()
        {
            var parentUsers = new List<User>(2);

            if (this.HasParent1)
                parentUsers.Add(new User
                {
                    FirstName = UserInfoHelper.GetFirstName(this.Parent1FullName).Truncate(255),
                    LastName = UserInfoHelper.GetLastName(this.Parent1FullName).Truncate(255),
                    IsSelfDetermined = true
                });

            if (this.HasParent2)
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
