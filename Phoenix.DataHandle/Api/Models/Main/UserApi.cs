using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class UserApi : IUser, IModelApi
    {
        private UserApi()
        {
            this.Courses = new List<CourseApi>();
            
            this.UserLogins = new List<IUserLogin>();
            this.BotFeedbacks = new List<IBotFeedback>();
            this.Broadcasts = new List<IBroadcast>();
            this.Grades = new List<IGrade>();
            this.Children = new List<IUser>();
            this.Lectures = new List<ILecture>();
            this.Parents = new List<IUser>();
            this.Roles = new List<IRole>();
            this.Schools = new List<ISchool>();
        }

        [JsonConstructor]
        public UserApi(int id, string username, string? email, string phoneNumber,
            string phoneCountryCode, int dependenceOrder, UserInfoApi userInfo, List<CourseApi>? courses)
            : this()
        {
            if (username is null)
                throw new ArgumentNullException(nameof(username));
            if (phoneNumber is null)
                throw new ArgumentNullException(nameof(phoneNumber));
            if (phoneCountryCode is null)
                throw new ArgumentNullException(nameof(phoneCountryCode));

            this.Id = id;
            this.UserName = username;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.DependenceOrder = dependenceOrder;
            this.UserInfo = userInfo;

            if (courses is not null)
                this.Courses = courses;
        }

        public UserApi(IUser user, bool include = false)
            : this(0, user.UserName, user.Email, user.PhoneNumber,
                  user.PhoneCountryCode, user.DependenceOrder, null!, null)
        {
            if (user is User user1)
                this.Id = user1.Id;

            if (user.UserInfo is not null)
                this.UserInfo = new UserInfoApi(user.UserInfo);

            // User is always included if it's not null
            if (!include)
                return;

            this.Courses = user.Courses.Select(c => new CourseApi(c)).ToList();
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; } = null!;

        [JsonProperty(PropertyName = "email")]
        public string? Email { get; }

        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; } = null!;

        [JsonProperty(PropertyName = "phone_country_code")]
        public string PhoneCountryCode { get; } = null!;

        [JsonProperty(PropertyName = "dependance_order")]
        public int DependenceOrder { get; }

        [JsonProperty(PropertyName = "user_info")]
        public UserInfoApi UserInfo { get; } = null!;
        
        [JsonProperty(PropertyName = "courses")]
        public List<CourseApi> Courses { get; }

        IUserInfo IUser.UserInfo => this.UserInfo;

        [JsonIgnore] 
        public IEnumerable<IUserLogin> UserLogins { get; }
        
        [JsonIgnore]
        public IEnumerable<IBotFeedback> BotFeedbacks { get; }
        
        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }
        
        [JsonIgnore]
        public IEnumerable<IGrade> Grades { get; }
        
        [JsonIgnore]
        public IEnumerable<IUser> Children { get; }

        IEnumerable<ICourse> IUser.Courses => this.Courses;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; }
        
        [JsonIgnore]
        public IEnumerable<IUser> Parents { get; }
        
        [JsonIgnore]
        public IEnumerable<IRole> Roles { get; }
        
        [JsonIgnore]
        public IEnumerable<ISchool> Schools { get; }
    }
}
