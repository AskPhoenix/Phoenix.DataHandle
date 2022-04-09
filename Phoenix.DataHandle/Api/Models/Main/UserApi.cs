using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class UserApi : IUser, IModelApi
    {
        [JsonConstructor]
        public UserApi(int id, string firstName, string lastName, string? fullName,
            bool isSelfDetermined)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = fullName;
            this.IsSelfDetermined = isSelfDetermined;
        }

        public UserApi(IUser user)
            : this(0, user.FirstName, user.LastName, user.FullName,
                  user.IsSelfDetermined)
        {
            if (user is User user1)
                this.Id = user1.Id;
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; }

        [JsonProperty(PropertyName = "full_name")]
        public string? FullName { get; }

        [JsonProperty(PropertyName = "is_self_determined")]
        public bool IsSelfDetermined { get; }

        [JsonIgnore]
        public IAspNetUser AspNetUser { get; } = null!;
    }
}
