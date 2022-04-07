using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class AspNetUserApi : IAspNetUser, IModelApi
    {
        private AspNetUserApi()
        {
            this.Courses = new List<CourseApi>();
            
            this.AspNetUserLogins = new List<IAspNetUserLogin>();
            this.BotFeedbacks = new List<IBotFeedback>();
            this.Broadcasts = new List<IBroadcast>();
            this.Grades = new List<IGrade>();
            this.Children = new List<IAspNetUser>();
            this.Lectures = new List<ILecture>();
            this.Parents = new List<IAspNetUser>();
            this.Roles = new List<IAspNetRole>();
            this.Schools = new List<ISchool>();
        }

        [JsonConstructor]
        public AspNetUserApi(int id, string username, string? email, string phoneNumber,
            int phoneNumberDependanceOrder, UserApi user, List<CourseApi>? courses)
            : this()
        {
            if (username is null)
                throw new ArgumentNullException(nameof(username));
            if (phoneNumber is null)
                throw new ArgumentNullException(nameof(phoneNumber));

            this.Id = id;
            this.UserName = username;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.PhoneNumberDependanceOrder = phoneNumberDependanceOrder;
            this.User = user;

            if (courses is not null)
                this.Courses = courses;
        }

        public AspNetUserApi(IAspNetUser aspNetUser, bool include = false)
            : this(0, aspNetUser.UserName, aspNetUser.Email, aspNetUser.PhoneNumber,
                  aspNetUser.PhoneNumberDependanceOrder, null!, null)
        {
            if (aspNetUser is AspNetUser aspNetUser1)
                this.Id = aspNetUser1.Id;

            if (aspNetUser.User is not null)
                this.User = new UserApi(aspNetUser.User);

            // User is always included if it's not null
            if (!include)
                return;

            this.Courses = aspNetUser.Courses.Select(c => new CourseApi(c)).ToList();
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; } = null!;

        [JsonProperty(PropertyName = "email")]
        public string? Email { get; }

        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; } = null!;

        [JsonProperty(PropertyName = "dependance_order")]
        public int PhoneNumberDependanceOrder { get; }

        [JsonProperty(PropertyName = "user")]
        public UserApi User { get; } = null!;
        
        [JsonProperty(PropertyName = "courses")]
        public List<CourseApi> Courses { get; }

        IUser IAspNetUser.User => this.User;

        [JsonIgnore] 
        public IEnumerable<IAspNetUserLogin> AspNetUserLogins { get; }
        
        [JsonIgnore]
        public IEnumerable<IBotFeedback> BotFeedbacks { get; }
        
        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }
        
        [JsonIgnore]
        public IEnumerable<IGrade> Grades { get; }
        
        [JsonIgnore]
        public IEnumerable<IAspNetUser> Children { get; }

        IEnumerable<ICourse> IAspNetUser.Courses => this.Courses;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; }
        
        [JsonIgnore]
        public IEnumerable<IAspNetUser> Parents { get; }
        
        [JsonIgnore]
        public IEnumerable<IAspNetRole> Roles { get; }
        
        [JsonIgnore]
        public IEnumerable<ISchool> Schools { get; }
    }
}
