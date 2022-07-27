using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Api.Models
{
    public class AspNetUserApi : IAspNetUserApi, IModelApi
    {
        private AspNetUserApi()
        {
            this.Roles = new string[1] { RoleRank.None.ToNormalizedString() }.ToList();
        }

        [JsonConstructor]
        public AspNetUserApi(int id, string username, string? email, string phoneNumber,
            UserApi user, List<string>? roles = null)
            : this()
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

            if (roles is not null && roles.Any())
                this.Roles = roles;
        }

        public AspNetUserApi(int id, IUserBase user, IAspNetUserBase aspNetUser, List<string>? roles = null)
            : this(id, aspNetUser.UserName, aspNetUser.Email, aspNetUser.PhoneNumber,
                  new(id, user), roles)
        {
        }

        public AspNetUserApi(User user, ApplicationUser appUser, List<string>? roles = null)
            : this(appUser.Id, user, appUser, roles)
        {
        }

        public ApplicationUser ToAppUser()
        {
            return new ApplicationUser()
            {
                Id = this.Id,
                UserName = this.UserName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber
            }.Normalize();
        }

        public ApplicationUser ToAppUser(ApplicationUser appUserToUpdate)
        {
            appUserToUpdate.UserName = this.UserName;
            appUserToUpdate.Email = this.Email;
            appUserToUpdate.PhoneNumber = this.PhoneNumber;

            return appUserToUpdate.Normalize();
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

        [JsonProperty("roles")]
        public List<string> Roles { get; }

        IUserApi IAspNetUserApi.User => this.User;
    }
}
