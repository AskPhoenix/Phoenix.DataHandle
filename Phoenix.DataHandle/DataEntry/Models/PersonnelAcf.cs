using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main.Types;
using System;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class PersonnelAcf : UserAcf, IModelAcf
    {
        [JsonConstructor]
        public PersonnelAcf(string full_name, string role, string phone, string? course_codes)
            : base(full_name, phone, 0, course_codes)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentNullException(nameof(role));
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentNullException(nameof(phone));

            this.Role = role.Replace(" ", "").ToRoleRank();
            if (!this.Role.IsStaffOrBackend())
                throw new ArgumentOutOfRangeException(nameof(role));

            this.IsSelfDetermined = true;

            this.RoleString = role;
        }

        [JsonProperty(PropertyName = "role")]
        public string RoleString { get; }
    }
}
