using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Utilities;
using System;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class ClientACF : UserACF, IModelACF
    {
        [JsonProperty(PropertyName = "student_full_name")]
        public override string FullName { get; }

        [JsonProperty(PropertyName = "course_codes")]
        public override string CourseCodesString { get; }

        [JsonProperty(PropertyName = "needs_parent_authorization")]
        public string NeedsParentAuthorizationString { get; }

        [JsonProperty(PropertyName = "student_phone")]
        public string? StudentPhoneNumber { get; }

        [JsonProperty(PropertyName = "parent1_full_name")]
        public string? Parent1FullName { get; }

        [JsonProperty(PropertyName = "parent1_phone")]
        public string? Parent1PhoneNumber { get; }

        [JsonProperty(PropertyName = "parent2_full_name")]
        public string? Parent2FullName { get; }

        [JsonProperty(PropertyName = "parent2_phone")]
        public string? Parent2PhoneNumber { get; }

        public bool IsSelfDetermined { get; }
        public override string PhoneNumber => this.IsSelfDetermined ? this.StudentPhoneNumber! : this.Parent1PhoneNumber!;
        public bool HasParent1 { get; }
        public bool HasParent2 { get; }
        public string? Parent1FirstName { get; }
        public string? Parent1LastName { get; }
        public string? Parent2FirstName { get; }
        public string? Parent2LastName { get; }

        [JsonConstructor]
#pragma warning disable CS8618 // Non-nullable properties (FullName, CourseCodesString) must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ClientACF(string fullName, string courseCodesString, string needsParentAuthString, string? studentPhone,
#pragma warning restore CS8618
            string? parent1FullName, string? parent1Phone, string? parent2FullName, string? parent2Phone)
            : base(fullName, courseCodesString)
        {
            if (string.IsNullOrWhiteSpace(needsParentAuthString))
                throw new ArgumentNullException(nameof(needsParentAuthString));

            this.NeedsParentAuthorizationString = needsParentAuthString;
            this.StudentPhoneNumber = studentPhone; // If student is not self-dependent, then their phone is not used or stored anywhere
            
            this.IsSelfDetermined = needsParentAuthString.Equals("No", StringComparison.InvariantCultureIgnoreCase);

            bool hasParent1 = !string.IsNullOrWhiteSpace(parent1FullName) && !string.IsNullOrWhiteSpace(parent1Phone);
            bool hasParent2 = !string.IsNullOrWhiteSpace(parent2FullName) && !string.IsNullOrWhiteSpace(parent2Phone);

            if (this.IsSelfDetermined && string.IsNullOrWhiteSpace(studentPhone))
                throw new ArgumentException("Cannot create user that is self-dependent without a phone number.");
            if (!this.IsSelfDetermined && !hasParent1 && !hasParent2)
                throw new ArgumentException("Cannot create user that is not self-dependent without at least a parent phone number.");

            if (hasParent1)
            {
                this.Parent1FullName = parent1FullName!.ToTitleCase();
                this.Parent1PhoneNumber = parent1Phone;
                
                this.HasParent1 = true;

                if (hasParent2)
                {
                    this.Parent2FullName = parent2FullName!.ToTitleCase();
                    this.Parent2FirstName = GetFirstName(this.Parent2FullName);
                    this.Parent2LastName = GetLastName(this.Parent2FullName);
                    this.Parent2PhoneNumber = parent2Phone;

                    this.HasParent2 = true;
                }
            }
            else if (!hasParent1 && hasParent2)
            {
                this.Parent1FullName = parent2FullName!.ToTitleCase();
                this.Parent1PhoneNumber = parent2Phone;

                this.HasParent1 = true;
                this.HasParent2 = false;
            }

            if (hasParent1 || hasParent2)
            {
                this.Parent1FirstName = GetFirstName(this.Parent1FullName!);
                this.Parent1LastName = GetLastName(this.Parent1FullName!);
            }
        }
    }
}
