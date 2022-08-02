using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Api.Models
{
    public class ApplicationUserApi : IApplicationUserApi, IModelApi
    {
        [JsonConstructor]
        public ApplicationUserApi(string? email, string? phoneNumber, UserApi user)
        {
            if (user is null)
                user = null!;

            this.Id = 0;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.User = user;

            this.Roles = new string[1] { RoleRank.None.ToNormalizedString() }.ToList();
        }

        public ApplicationUserApi(IUserBase user, IApplicationUserBase aspNetUser,
            List<RoleRank>? roleRanks = null)
            : this(aspNetUser.Email, aspNetUser.PhoneNumber, new(user))
        {
            if (roleRanks is not null)
                this.Roles = roleRanks.Select(rr => rr.ToNormalizedString()).ToList();

            this.UserName = aspNetUser.UserName;
        }

        public ApplicationUserApi(User user, ApplicationUser appUser, List<RoleRank>? roleRanks = null)
            : this((IUserBase)user, (IApplicationUserBase)appUser, roleRanks)
        {
            this.Id = appUser.Id;
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

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("username")]
        public string UserName { get; } = null!;

        [JsonProperty("email")]
        public string? Email { get; }

        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; }

        [JsonProperty("user_info", Required = Required.Always)]
        public UserApi User { get; } = null!;

        [JsonProperty("roles")]
        public List<string> Roles { get; }

        IUserApi IApplicationUserApi.User => this.User;
    }
}
