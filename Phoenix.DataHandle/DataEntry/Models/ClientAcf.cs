using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class ClientAcf : UserAcf, IModelAcf
    {
        // Constructor for Parent Client
        private ClientAcf(string fullName, string phone)
            : base(fullName, phone, 0)
        {
            this.Role = RoleRank.Parent;
            this.IsSelfDetermined = true;
        }

        // Constructor for Student Client
        [JsonConstructor]
        public ClientAcf(string student_full_name, string? course_codes, string needs_parent_authorization,
            string? student_phone, string? parent1_full_name, string? parent1_phone, 
            string? parent2_full_name, string? parent2_phone, int dependenceOrder)
            : base(student_full_name, null!, dependenceOrder, course_codes)
        {
            if (string.IsNullOrWhiteSpace(needs_parent_authorization))
                throw new ArgumentNullException(nameof(needs_parent_authorization));

            if (string.IsNullOrWhiteSpace(student_phone))
                student_phone = null;
            if (string.IsNullOrWhiteSpace(parent1_phone))
                parent1_phone = null;
            if (string.IsNullOrWhiteSpace(parent2_phone))
                parent2_phone = null;

            if (student_phone is null && parent1_phone is null && parent2_phone is null)
                throw new InvalidOperationException(
                    "There must be at least one phone number for a student or their parents.");

            // If student has no phone, then the StudentPhoneString is null and the PhoneString
            // takes the phone of the first available parent
            this.PhoneString = student_phone ?? parent1_phone ?? parent2_phone!;
            this.StudentPhoneString = student_phone;

            if (!string.IsNullOrWhiteSpace(parent1_full_name) && !string.IsNullOrWhiteSpace(parent1_phone))
                this.Parent1 = new ClientAcf(parent1_full_name, parent1_phone);
            if (!string.IsNullOrWhiteSpace(parent2_full_name) && !string.IsNullOrWhiteSpace(parent2_phone))
                this.Parent2 = new ClientAcf(parent2_full_name, parent2_phone);

            this.IsSelfDetermined = needs_parent_authorization.Equals("No", StringComparison.InvariantCultureIgnoreCase);

            if (!this.IsSelfDetermined && !this.HasParent1 && !this.HasParent2)
                throw new InvalidOperationException("Cannot create non self-determined student without any parent");
            if (this.IsSelfDetermined && string.IsNullOrWhiteSpace(student_phone))
                throw new ArgumentException("Cannot create self-determined student without a student phone number.");

            this.Role = RoleRank.Student;

            this.NeedsParentAuthorization = needs_parent_authorization;

            // Cannot set Dependance Order here
        }

        [JsonProperty(PropertyName = "student_full_name")]
        public string StudentFullName => FullName;

        [JsonProperty(PropertyName = "needs_parent_authorization")]
        public string NeedsParentAuthorization { get; } = null!;

        [JsonProperty(PropertyName = "student_phone")]
        public string? StudentPhoneString { get; }

        [JsonProperty(PropertyName = "parent1_full_name")]
        public string? Parent1FullName => HasParent1 ? Parent1!.FullName : null;

        [JsonProperty(PropertyName = "parent1_phone")]
        public string? Parent1Phone => HasParent1 ? Parent1!.PhoneString : null;

        [JsonProperty(PropertyName = "parent2_full_name")]
        public string? Parent2FullName => HasParent2 ? Parent2!.FullName : null;

        [JsonProperty(PropertyName = "parent2_phone")]
        public string? Parent2Phone => HasParent2 ? Parent2!.PhoneString : null;

        [JsonIgnore]
        public ClientAcf? Parent1 { get; }

        [JsonIgnore]
        public ClientAcf? Parent2 { get; }

        [JsonIgnore]
        public bool HasParent1 => this.Parent1 != null;

        [JsonIgnore]
        public bool HasParent2 => this.Parent2 != null;
    }
}
