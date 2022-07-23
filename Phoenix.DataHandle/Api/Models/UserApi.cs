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
        public UserApi(int id, string firstName, string lastName, string fullName,
            bool isSelfDetermined, int dependenceOrder)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));

            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = fullName;
            this.IsSelfDetermined = isSelfDetermined;
            this.DependenceOrder = dependenceOrder;
        }

        public UserApi(int id, IUserBase user)
            : this(id, user.FirstName, user.LastName, user.FullName,
                  user.IsSelfDetermined, user.DependenceOrder)
        {
        }

        public UserApi(User user)
            : this(user.AspNetUserId, user)
        {
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

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; } = null!;

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; } = null!;

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; } = null!;

        [JsonProperty(PropertyName = "is_self_determined")]
        public bool IsSelfDetermined { get; }

        [JsonProperty(PropertyName = "dependance_order")]
        public int DependenceOrder { get; }
    }
}
