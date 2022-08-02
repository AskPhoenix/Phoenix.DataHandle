using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class UserApi : IUserApi, IModelApi
    {
        [JsonConstructor]
        public UserApi(string firstName, string lastName, string fullName, bool isSelfDetermined)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                firstName = null!;
            if (string.IsNullOrWhiteSpace(lastName))
                lastName = null!;
            if (string.IsNullOrWhiteSpace(fullName))
                fullName = null!;

            this.Id = 0;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = fullName;
            this.IsSelfDetermined = isSelfDetermined;
        }

        public UserApi(IUserBase user)
            : this(user.FirstName, user.LastName, user.FullName, user.IsSelfDetermined)
        {
            this.DependenceOrder = user.DependenceOrder;
        }

        public UserApi(User user)
            : this((IUserBase)user)
        {
            this.Id = user.AspNetUserId;
        }

        public User ToUser()
        {
            return new User()
            {
                AspNetUserId = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                IsSelfDetermined = this.IsSelfDetermined,
                DependenceOrder = this.DependenceOrder
            };
        }

        public User ToUser(User userToUpdate)
        {
            userToUpdate.FirstName = this.FirstName;
            userToUpdate.LastName = this.LastName;
            userToUpdate.IsSelfDetermined = this.IsSelfDetermined;
            userToUpdate.DependenceOrder = this.DependenceOrder;

            return userToUpdate;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("first_name", Required = Required.Always)]
        public string FirstName { get; } = null!;

        [JsonProperty("last_name", Required = Required.Always)]
        public string LastName { get; } = null!;

        [JsonProperty("full_name", Required = Required.Always)]
        public string FullName { get; } = null!;

        [JsonProperty("is_self_determined", Required = Required.Always)]
        public bool IsSelfDetermined { get; }

        [JsonProperty("dependance_order")]
        public int DependenceOrder { get; }
    }
}
