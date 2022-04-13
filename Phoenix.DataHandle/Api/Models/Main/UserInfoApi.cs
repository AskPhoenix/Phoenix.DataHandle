using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class UserInfoApi : IUserInfo, IModelApi
    {
        [JsonConstructor]
        public UserInfoApi(int id, string firstName, string lastName, string fullName,
            bool isSelfDetermined)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = fullName;
            this.IsSelfDetermined = isSelfDetermined;
        }

        public UserInfoApi(IUserInfo userInfo)
            : this(0, userInfo.FirstName, userInfo.LastName, userInfo.FullName,
                  userInfo.IsSelfDetermined)
        {
            if (userInfo is UserInfo user1)
                this.Id = user1.UserId;
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; }

        [JsonProperty(PropertyName = "is_self_determined")]
        public bool IsSelfDetermined { get; }

        [JsonIgnore]
        public IUser User { get; } = null!;
    }
}
