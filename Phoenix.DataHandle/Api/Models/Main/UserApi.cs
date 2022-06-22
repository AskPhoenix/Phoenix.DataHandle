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

            this.BotFeedbacks = new List<IBotFeedback>();
            this.Broadcasts = new List<IBroadcast>();
            this.Grades = new List<IGrade>();
            this.OneTimeCodes = new List<IOneTimeCode>();
            this.UserConnections = new List<IUserConnection>();
            this.Children = new List<IUser>();
            this.Lectures = new List<ILecture>();
            this.Parents = new List<IUser>();
            this.Schools = new List<ISchool>();
        }

        [JsonConstructor]
        public UserApi(int id, string firstName, string lastName, string fullName,
            bool isSelfDetermined, int dependenceOrder, AspNetUserApi aspNetUser, List<CourseApi>? courses)
            : this()
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
            this.AspNetUser = aspNetUser;

            if (courses is not null)
                this.Courses = courses;
        }

        public UserApi(IUser user, bool include = false)
            : this(0, user.FirstName, user.LastName, user.FullName,
                  user.IsSelfDetermined, user.DependenceOrder, null!, null)
        {
            if (user is User user1)
                this.Id = user1.AspNetUserId;

            // TODO: Separate AspNetUserApi

            //if (user.AspNetUser is not null)
            //    this.AspNetUser = new AspNetUserApi(user.AspNetUser);

            // AspNetUser is always included if it's not null
            if (!include)
                return;

            this.Courses = user.Courses.Select(c => new CourseApi(c)).ToList();
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

        [JsonProperty(PropertyName = "aspnet_user")]
        public AspNetUserApi AspNetUser { get; } = null!;

        [JsonProperty(PropertyName = "courses")]
        public List<CourseApi> Courses { get; }

        [JsonIgnore]
        public IEnumerable<IBotFeedback> BotFeedbacks { get; }

        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }

        [JsonIgnore]
        public IEnumerable<IGrade> Grades { get; }

        [JsonIgnore]
        public IEnumerable<IOneTimeCode> OneTimeCodes { get; }
        
        [JsonIgnore]
        public IEnumerable<IUserConnection> UserConnections { get; }


        [JsonIgnore]
        public IEnumerable<IUser> Children { get; }

        IEnumerable<ICourse> IUser.Courses => this.Courses;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; }

        [JsonIgnore]
        public IEnumerable<IUser> Parents { get; }

        [JsonIgnore]
        public IEnumerable<ISchool> Schools { get; }
    }
}
