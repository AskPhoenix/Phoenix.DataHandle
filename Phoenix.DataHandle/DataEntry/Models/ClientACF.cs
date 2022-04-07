using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main;
using System;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class ClientACF : UserACF, IModelACF
    {
        [JsonIgnore]
        public ClientACF? Parent1 { get; }

        [JsonIgnore]
        public ClientACF? Parent2 { get; }

        [JsonIgnore]
        public bool HasParent1 => this.Parent1 != null;

        [JsonIgnore]
        public bool HasParent2 => this.Parent2 != null;

        // Constructor for Parent Client
        private ClientACF(string fullName, string phone)
            : base(fullName, phone)
        {
            this.Role = Role.Parent;
            this.IsSelfDetermined = true;
            this.DependanceOrder = 0;
        }

        // Constructor for Student Client
        [JsonConstructor]
        public ClientACF(string studentFullName, string? courseCodes, string needsParentAuthorization, string? studentPhone,
            string? parent1FullName, string? parent1Phone, string? parent2FullName, string? parent2Phone)
            : base(studentFullName, studentPhone, courseCodes)
        {
            if (string.IsNullOrWhiteSpace(needsParentAuthorization))
                throw new ArgumentNullException(nameof(needsParentAuthorization));

            if (!string.IsNullOrWhiteSpace(parent1FullName) && !string.IsNullOrWhiteSpace(parent1Phone))
                this.Parent1 = new ClientACF(parent1FullName, parent1Phone);
            if (!string.IsNullOrWhiteSpace(parent2FullName) && !string.IsNullOrWhiteSpace(parent2Phone))
                this.Parent2 = new ClientACF(parent2FullName, parent2Phone);

            this.IsSelfDetermined = needsParentAuthorization.Equals("No", StringComparison.InvariantCultureIgnoreCase);

            if (!this.IsSelfDetermined && !this.HasParent1 && !this.HasParent2)
                throw new InvalidOperationException("Cannot create non self-determined student without any parent");
            if (this.IsSelfDetermined && string.IsNullOrWhiteSpace(studentPhone))
                throw new ArgumentException("Cannot create self-determined student without a student phone number.");

            this.Role = Role.Student;

            // Cannot set Dependance Order here
        }
    }
}
