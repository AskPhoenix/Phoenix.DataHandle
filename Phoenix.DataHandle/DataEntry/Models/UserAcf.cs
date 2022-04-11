using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using Phoenix.DataHandle.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public abstract class UserAcf : IUser
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

            this.AspNetUser = new AspNetUser
            {
                PhoneNumber = phone
            };

            this.PhoneString = this.AspNetUser.PhoneNumber;
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

        // TODO: Check if this can be translated to SQL query. If yes, delete the other GetUniqueExpression method
        // Unique expression is linked with the AspNetUser object
        public Expression<Func<AspNetUser, bool>> GetUniqueExpression(string phoneCountryCode, int dependanceOrder) => u =>
            u.PhoneCountryCode == phoneCountryCode &&
            u.PhoneNumber == this.AspNetUser.PhoneNumber && 
            u.DependenceOrder == dependanceOrder;
        
        public Expression<Func<AspNetUser, bool>> GetUniqueExpression()
        {
            if (this.AspNetUser is null)
                throw new InvalidOperationException(
                    $"Properties {nameof(PhoneCountryCode)} and {nameof(DependenceOrder)} must be set first.");

            return GetUniqueExpression(this.AspNetUser.PhoneCountryCode, this.AspNetUser.DependenceOrder);
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
        public Role Role { get; protected set; }

        [JsonIgnore]
        public bool IsSelfDetermined { get; protected set; }

        [JsonIgnore]
        public List<short> CourseCodes { get; }

        [JsonIgnore]
        public IAspNetUser AspNetUser 
        {
            get 
            {
                if (this.PhoneCountryCode is null || this.DependenceOrder is null)
                    return null!;

                return aspNetUser!;
            }
            private set { aspNetUser = value; }
        }
        private IAspNetUser? aspNetUser = null;

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

                this.AspNetUser = new AspNetUser
                {
                    PhoneNumber = this.AspNetUser.PhoneNumber,
                    PhoneCountryCode = value,
                    DependenceOrder = this.AspNetUser.DependenceOrder
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
                    this.AspNetUser = new AspNetUser
                    {
                        PhoneNumber = this.AspNetUser.PhoneNumber,
                        PhoneCountryCode= this.AspNetUser.PhoneCountryCode,
                        DependenceOrder = dependenceOrder.Value
                    };
                }
            }
        }
        private int? dependenceOrder = null;
    }
}
