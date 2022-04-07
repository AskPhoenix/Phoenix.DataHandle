using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main;
using System;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class PersonnelACF : UserACF, IModelACF
    {
        [JsonConstructor]
        public PersonnelACF(string fullName, string role, string phone, string? courseCodes)
            : base(fullName, phone, courseCodes)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentNullException(nameof(role));
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentNullException(nameof(phone));

            this.Role = role.Replace(" ", "").ToRole();
            if (!this.Role.IsPersonnel())
                throw new ArgumentOutOfRangeException(nameof(role));

            this.IsSelfDetermined = true;
            this.DependanceOrder = 0;
        }
    }
}
