using Newtonsoft.Json;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Types;
using Phoenix.DataHandle.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public abstract class UserAcf : IUserInfo
    {
        private UserAcf()
        {
            this.CourseCodes = new List<short>();

            this.BotFeedbacks = new List<IBotFeedback>();
            this.Broadcasts = new List<IBroadcast>();
            this.Grades = new List<IGrade>();
            this.OneTimeCodes = new List<IOneTimeCode>();
            this.Children = new List<IUserInfo>();
            this.Courses = new List<ICourse>();
            this.Lectures = new List<ILecture>();
            this.Parents = new List<IUserInfo>();
            this.Schools = new List<ISchool>();
        }

        public UserAcf(string fullName, string phone, int dependenceOrder)
            : this()
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));

            this.FullName = fullName.ToTitleCase();
            this.FirstName = this.ResolveFirstName();
            this.LastName = this.ResolveLastName();

            this.AspNetUser = new ApplicationUser
            {
                PhoneNumber = phone
            };

            this.PhoneString = this.AspNetUser.PhoneNumber;

            if (dependenceOrder < 0)
                throw new ArgumentOutOfRangeException(nameof(DependenceOrder));
            if (this.IsSelfDetermined && dependenceOrder != 0)
                throw new InvalidOperationException(
                    $"Cannot set {nameof(DependenceOrder)} to a non-zero value for a self-setermined user.");

            this.DependenceOrder = dependenceOrder;
        }

        public UserAcf(string fullName, string phone, int dependenceOrder, string? courseCodes)
            : this(fullName, phone, dependenceOrder)
        {
            // Accept empty course codes string in order to register the user
            // in all the courses of their school
            if (!string.IsNullOrWhiteSpace(courseCodes))
            {
                this.CourseCodes = courseCodes.
                    Split(',', StringSplitOptions.RemoveEmptyEntries).
                    Select(cc => short.Parse(cc.Trim(), CultureInfo.InvariantCulture)).
                    ToList();

                this.CourseCodesString = courseCodes;
            }
        }

        [JsonIgnore]
        public string FirstName { get; } = null!;

        [JsonIgnore]
        public string LastName { get; } = null!;

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; } = null!;

        [JsonProperty(PropertyName = "phone")]
        public string PhoneString { get; } = null!;

        [JsonProperty(PropertyName = "course_codes")]
        public string CourseCodesString { get; } = string.Empty;

        [JsonIgnore]
        public RoleRank Role { get; protected set; }

        [JsonIgnore]
        public bool IsSelfDetermined { get; protected set; }

        [JsonIgnore]
        public int DependenceOrder { get; set; }

        [JsonIgnore]
        public List<short> CourseCodes { get; }


        [JsonIgnore]
        public IAspNetUser AspNetUser { get; } = null!;

        [JsonIgnore]
        public IEnumerable<IBotFeedback> BotFeedbacks { get; }

        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }

        [JsonIgnore]
        public IEnumerable<IGrade> Grades { get; }

        [JsonIgnore]
        public IEnumerable<IOneTimeCode> OneTimeCodes { get; }


        [JsonIgnore]
        public IEnumerable<IUserInfo> Children { get; }

        [JsonIgnore]
        public IEnumerable<ICourse> Courses { get; }

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; }

        [JsonIgnore]
        public IEnumerable<IUserInfo> Parents { get; }

        [JsonIgnore]
        public IEnumerable<ISchool> Schools { get; }
    }
}
