using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class AspNetUserApi : IAspNetUserApi, IModelApi
    {
        [JsonConstructor]
        public AspNetUserApi(int id, string username, string? email, string phoneNumber, UserApi user)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            this.Id = id;
            this.UserName = username;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.User = user;
        }

        public AspNetUserApi(int id, IUserBase user, IAspNetUserBase aspNetUser)
            : this(id, aspNetUser.UserName, aspNetUser.Email, aspNetUser.PhoneNumber, new(id, user))
        {
        }

        public AspNetUserApi(User user, ApplicationUser appUser)
            : this(appUser.Id, user, appUser)
        {
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; } = null!;

        [JsonProperty(PropertyName = "email")]
        public string? Email { get; }

        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; } = null!;

        [JsonProperty(PropertyName = "user_info")]
        public UserApi User { get; } = null!;

        IUserApi IAspNetUserApi.User => this.User;
    }
}
