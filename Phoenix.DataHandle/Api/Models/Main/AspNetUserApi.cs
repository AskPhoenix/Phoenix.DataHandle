using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Entities;
using System;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class AspNetUserApi : IAspNetUser, IModelApi
    {
        [JsonConstructor]
        public AspNetUserApi(int id, string username, string? email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            this.Id = id;
            this.UserName = username;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public AspNetUserApi(IAspNetUser aspNetUser)
            : this(0, aspNetUser.UserName, aspNetUser.Email, aspNetUser.PhoneNumber)
        {
            if (aspNetUser is ApplicationUser appUser)
                this.Id = appUser.Id;
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; } = null!;

        [JsonProperty(PropertyName = "email")]
        public string? Email { get; }

        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; } = null!;

        [JsonIgnore]
        public IUser User { get; } = null!;
    }
}
