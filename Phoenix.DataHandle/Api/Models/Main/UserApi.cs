using System;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class UserApi : IUser, IModelApi
    {
        [JsonConstructor]
        public UserApi(AspNetUserApi aspNetUser, string? firstName, string? lastName, string? fullName,
            bool termsAccepted, bool isSelfDetermined)
        {
            if (aspNetUser is null)
                throw new ArgumentNullException(nameof(aspNetUser));

            this.AspNetUser = aspNetUser;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = fullName;
            this.TermsAccepted = termsAccepted;
            this.IsSelfDetermined = isSelfDetermined;
        }

        public UserApi(IUser user)
            : this(new AspNetUserApi(user.AspNetUser), user.FirstName, user.LastName, user.FullName,
                  user.TermsAccepted, user.IsSelfDetermined)
        {
        }

        [JsonProperty(PropertyName = "aspnet_user")]
        public AspNetUserApi AspNetUser { get; } = null!;

        [JsonProperty(PropertyName = "first_name")]
        public string? FirstName { get; }

        [JsonProperty(PropertyName = "last_name")]
        public string? LastName { get; }

        [JsonProperty(PropertyName = "full_name")]
        public string? FullName { get; }

        [JsonProperty(PropertyName = "terms_accepted")]
        public bool TermsAccepted { get; }

        [JsonProperty(PropertyName = "is_self_determined")]
        public bool IsSelfDetermined { get; }


        int IModelApi.Id => AspNetUser.Id;

        IAspNetUser IUser.AspNetUser => this.AspNetUser;
    }
}
