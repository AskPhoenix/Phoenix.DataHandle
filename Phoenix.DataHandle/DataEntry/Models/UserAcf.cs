﻿using Newtonsoft.Json;
using Phoenix.DataHandle.Base;
using Phoenix.DataHandle.DataEntry.Entities;
using Phoenix.DataHandle.DataEntry.Types.Uniques;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using Phoenix.DataHandle.Utilities;
using System.Globalization;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public abstract class UserAcf : IUserAcf
    {
        private UserAcf()
        {
            this.CourseCodes = new List<short>();
        }

        public UserAcf(string fullName, string phone, int dependenceOrder)
            : this()
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));
            // Nullability of phone argument is checked in the derived constructors

            this.FullName = fullName.ToTitleCase();
            this.FirstName = this.ResolveFirstName();
            this.LastName = this.ResolveLastName();

            this.PhoneString = phone;

            if (dependenceOrder < 0)
                throw new ArgumentOutOfRangeException(nameof(DependenceOrder));
            if (this.IsSelfDetermined && dependenceOrder != 0)
                throw new InvalidOperationException(
                    $"Cannot set {nameof(DependenceOrder)} to a non-zero value for a self-determined user.");

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

        public User ToUser(int aspNetUserId)
        {
            return new()
            {
                AspNetUserId = aspNetUserId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DependenceOrder = this.DependenceOrder,
                IsSelfDetermined = this.IsSelfDetermined,
                HasAcceptedTerms = false
            };
        }

        public User ToUser(User userToUpdate, int aspNetUserId)
        {
            if (userToUpdate is null)
                throw new ArgumentNullException(nameof(userToUpdate));

            userToUpdate.AspNetUserId = aspNetUserId;
            userToUpdate.FirstName = this.FirstName;
            userToUpdate.LastName = this.LastName;
            userToUpdate.DependenceOrder = this.DependenceOrder;
            userToUpdate.IsSelfDetermined = this.IsSelfDetermined;

            return userToUpdate;
        }

        public string GenerateUserName(SchoolUnique schoolUq, string phoneCountryCode)
        {
            return this.GenerateUserName(new int[1] { schoolUq.Code },
                PrependPhoneCountryCode(this.PhoneString, phoneCountryCode)!);
        }

        public static string PrependPhoneCountryCode(string phone, string phoneCountryCode)
        {
            string trimmedPhone = phone.TrimStart('0', '+');

            if (trimmedPhone.StartsWith(phoneCountryCode.TrimStart('0', '+')))
                return "+" + trimmedPhone;

            if (!phone.Equals(trimmedPhone))
                return "+" + trimmedPhone;

            return phoneCountryCode + phone;
        }

        [JsonIgnore]
        public string FirstName { get; } = null!;

        [JsonIgnore]
        public string LastName { get; } = null!;

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; } = null!;

        [JsonIgnore]
        public bool IsSelfDetermined { get; protected set; }

        [JsonIgnore]
        public int DependenceOrder { get; set; }


        [JsonProperty(PropertyName = "phone")]
        public string PhoneString { get; protected set; } = null!;

        [JsonProperty(PropertyName = "course_codes")]
        public string CourseCodesString { get; } = string.Empty;


        [JsonIgnore]
        public RoleRank Role { get; protected set; }


        [JsonIgnore]
        public List<short> CourseCodes { get; }
    }
}
