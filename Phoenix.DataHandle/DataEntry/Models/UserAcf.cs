using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
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
        }

        public UserAcf(string fullName, string phone)
            : this()
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));

            this.FullName = fullName.ToTitleCase();
            this.FirstName = this.ResolveFirstName();
            this.LastName = this.ResolveLastName();

            this.User = new User
            {
                PhoneNumber = phone
            };

            this.PhoneString = this.User.PhoneNumber;
        }

        public UserAcf(string fullName, string phone, string? courseCodes)
            : this(fullName, phone)
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
        public List<short> CourseCodes { get; }

        [JsonIgnore]
        public IUser User 
        {
            get 
            {
                if (this.PhoneCountryCode is null || this.DependenceOrder is null)
                    return null!;

                return user!;
            }
            private set { user = value; }
        }
        private IUser? user = null;

        [JsonIgnore]
        public string? PhoneCountryCode 
        {
            get
            {
                return phoneCountryCode;
            }
            set
            {
                if (value is null)
                    return;

                phoneCountryCode = value;

                this.User = new User
                {
                    PhoneNumber = this.User.PhoneNumber,
                    PhoneCountryCode = value,
                    DependenceOrder = this.User.DependenceOrder
                };
            }
        }
        private string? phoneCountryCode = null;

        [JsonIgnore]
        public int? DependenceOrder 
        {
            get 
            {
                if (this.IsSelfDetermined)
                    return 0;

                return dependenceOrder;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(DependenceOrder));
                if (this.IsSelfDetermined && value != 0)
                    throw new InvalidOperationException(
                        $"Cannot set {nameof(DependenceOrder)} to a non-zero value for a self-setermined user.");

                dependenceOrder = value;

                if (dependenceOrder != null)
                {
                    this.User = new User
                    {
                        PhoneNumber = this.User.PhoneNumber,
                        PhoneCountryCode= this.User.PhoneCountryCode,
                        DependenceOrder = dependenceOrder.Value
                    };
                }
            }
        }
        private int? dependenceOrder = null;
    }
}
