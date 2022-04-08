using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main;
using System;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class ClientAcf : UserAcf, IModelAcf
    {
        [JsonIgnore]
        public ClientAcf? Parent1 { get; }

        [JsonIgnore]
        public ClientAcf? Parent2 { get; }

        [JsonIgnore]
        public bool HasParent1 => this.Parent1 != null;

        [JsonIgnore]
        public bool HasParent2 => this.Parent2 != null;

        // Constructor for Parent Client
        private ClientAcf(string fullName, string phone)
            : base(fullName, phone)
        {
            this.Role = Role.Parent;
            this.IsSelfDetermined = true;
            this.DependanceOrder = 0;
        }

        // Constructor for Student Client
        [JsonConstructor]
        public ClientAcf(string student_full_name, string? course_codes, string needs_parent_authorization,
            string? student_phone, string? parent1_full_name, string? parent1_phone, 
            string? parent2_full_name, string? parent2_phone)
            : base(student_full_name, student_phone, course_codes)
        {
            if (string.IsNullOrWhiteSpace(needs_parent_authorization))
                throw new ArgumentNullException(nameof(needs_parent_authorization));

            if (!string.IsNullOrWhiteSpace(parent1_full_name) && !string.IsNullOrWhiteSpace(parent1_phone))
                this.Parent1 = new ClientAcf(parent1_full_name, parent1_phone);
            if (!string.IsNullOrWhiteSpace(parent2_full_name) && !string.IsNullOrWhiteSpace(parent2_phone))
                this.Parent2 = new ClientAcf(parent2_full_name, parent2_phone);

            this.IsSelfDetermined = needs_parent_authorization.Equals("No", StringComparison.InvariantCultureIgnoreCase);

            if (!this.IsSelfDetermined && !this.HasParent1 && !this.HasParent2)
                throw new InvalidOperationException("Cannot create non self-determined student without any parent");
            if (this.IsSelfDetermined && string.IsNullOrWhiteSpace(student_phone))
                throw new ArgumentException("Cannot create self-determined student without a student phone number.");

            this.Role = Role.Student;

            // Cannot set Dependance Order here
        }
    }
}
