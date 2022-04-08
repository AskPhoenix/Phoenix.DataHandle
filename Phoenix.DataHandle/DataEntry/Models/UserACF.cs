using Newtonsoft.Json;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
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

        public UserAcf(string fullName, string? phone)
            : this()
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));

            this.FullName = fullName.ToTitleCase();
            this.FirstName = GetName(this.FullName, selFirstName: true);
            this.LastName = GetName(this.FullName, selFirstName: false);

            this.AspNetUser = new AspNetUser
            {
                PhoneNumber = phone
            };
        }

        public UserAcf(string fullName, string? phone, string? courseCodes)
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
            }
        }

        // Unique expression is linked with the AspNetUser object
        public Expression<Func<AspNetUser, bool>> GetUniqueExpression(int dependanceOrder) => u =>
            u.PhoneNumber == this.AspNetUser.PhoneNumber && u.PhoneNumberDependanceOrder == dependanceOrder;

        // TODO: Check if this can be translated to SQL query. If yes, delete the other GetUniqueExpression method
        public Expression<Func<AspNetUser, bool>> GetUniqueExpression()
        {
            if (!this.IsSelfDetermined && this.DependanceOrder is null)
                throw new InvalidOperationException("");

            return GetUniqueExpression(this.DependanceOrder ?? 0);
        }

        // TODO: Move these in IUser
        protected static string GetName(string fullName, bool selFirstName, bool truncate = true)
        {
            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (!selFirstName && names.Length == 1)
                return string.Empty;

            string tore;
            if (selFirstName)
                tore = string.Join(' ', names.Take((int)Math.Ceiling(names.Length / 2.0)));
            else
                tore = string.Join(' ', names.TakeLast(names.Length / 2));

            if (truncate)
                tore = tore.Truncate(255);

            return tore;
        }

        // Dependance order separates non-self-dependent users. The phone owner takes a value of 0.
        public string GetUserName(int schoolCode)
        {
            if (this.DependanceOrder is null)
                throw new InvalidOperationException("User's dependance order number must be set first.");

            return "S" + schoolCode + "_P" + this.AspNetUser.PhoneNumber + "_N" + this.FirstName + "_O" + this.DependanceOrder;
        }


        // TODO: Check if JsonProperty names are inherited to children classes

        [JsonIgnore]
        public string FirstName { get; } = null!;

        [JsonIgnore]
        public string LastName { get; } = null!;

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; } = null!;

        [JsonIgnore]
        public Role Role { get; protected set; }

        [JsonIgnore]
        public bool IsSelfDetermined { get; protected set; }

        [JsonIgnore]
        public List<short> CourseCodes { get; }

        [JsonIgnore]
        public IAspNetUser AspNetUser { get; private set; } = null!;

        [JsonIgnore]
        public int? DependanceOrder {
            get 
            {
                if (this.IsSelfDetermined)
                    return 0;

                return dependanceOrder;
            }
            set
            {
                if (this.IsSelfDetermined && value != 0)
                    throw new InvalidOperationException("Cannot set Dependance Order to a non-zero value for a Self-Determined user");

                dependanceOrder = value;

                if (dependanceOrder != null)
                {
                    this.AspNetUser = new AspNetUser
                    {
                        PhoneNumber = this.AspNetUser.PhoneNumber,
                        PhoneNumberDependanceOrder = dependanceOrder.Value
                    };
                }
            }
        }
        private int? dependanceOrder = null;
    }
}
