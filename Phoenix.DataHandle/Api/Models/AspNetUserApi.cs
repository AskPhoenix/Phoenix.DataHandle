﻿using Newtonsoft.Json;
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
                username = null!;
            if (string.IsNullOrWhiteSpace(phoneNumber))
                phoneNumber = null!;
            if (user is null)
                user = null!;

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

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("username", Required = Required.Always)]
        public string UserName { get; } = null!;

        [JsonProperty("email")]
        public string? Email { get; }

        [JsonProperty("phone_number", Required = Required.Always)]
        public string PhoneNumber { get; } = null!;

        [JsonProperty("user_info", Required = Required.Always)]
        public UserApi User { get; } = null!;

        [JsonProperty("roles", Required = Required.Always)]
        public List<string> Roles { get; }

        IUserApi IAspNetUserApi.User => this.User;
    }
}
