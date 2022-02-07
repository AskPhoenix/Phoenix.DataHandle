using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main;
using System;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class PersonnelACF : UserACF, IModelACF
    {
        [JsonProperty(PropertyName = "full_name")]
        public override string FullName { get; }

        [JsonProperty(PropertyName = "role")]
        public string RoleString { get; }

        [JsonProperty(PropertyName = "phone")]
        public override string PhoneNumber { get; }

        [JsonProperty(PropertyName = "course_codes")]
        public override string CourseCodesString { get; }

        public Role RoleType { get; }
        

        [JsonConstructor]
#pragma warning disable CS8618 // Non-nullable properties (FullName, CourseCodesString) must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PersonnelACF(string fullName, string roleString, string phoneNumber, string courseCodesString)
#pragma warning restore CS8618
            : base(fullName, courseCodesString)
        {
            if (string.IsNullOrWhiteSpace(roleString))
                throw new ArgumentNullException(nameof(roleString));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            this.RoleString = roleString;
            this.PhoneNumber = phoneNumber;

            this.RoleType = this.RoleString.Replace(" ", "").ToRole();
            if (!this.RoleType.IsPersonnel())
                throw new ArgumentOutOfRangeException(nameof(roleString));
        }
    }
}
