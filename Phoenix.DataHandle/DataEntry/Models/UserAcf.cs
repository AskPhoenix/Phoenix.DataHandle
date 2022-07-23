using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Types.Uniques;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using Phoenix.DataHandle.Utilities;
using System.Globalization;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public abstract class UserAcf : IUser
    {
        private UserAcf()
        {
            this.CourseCodes = new List<short>();
            this.Courses = new HashSet<Course>();
            this.Schools = new HashSet<School>();

            this.BotFeedbacks = Enumerable.Empty<IBotFeedback>();
            this.Broadcasts = Enumerable.Empty<IBroadcast>();
            this.Grades = Enumerable.Empty<IGrade>();
            this.OneTimeCodes = Enumerable.Empty<IOneTimeCode>();
            this.UserConnections = Enumerable.Empty<IUserConnection>();
            this.Children = Enumerable.Empty<IUser>();
            this.Lectures = Enumerable.Empty<ILecture>();
            this.Parents = Enumerable.Empty<IUser>();
        }

        public UserAcf(string fullName, string phone, int dependenceOrder)
            : this()
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));
            // Nullability of phone argument is checked in the derived constructors

            this.FullName = fullName.ToTitleCase();
            this.FirstName = this.ResolveFirstName();
            this.LastName = this.ResolveLastName();

            this.PhoneString = phone;

            if (dependenceOrder < 0)
                throw new ArgumentOutOfRangeException(nameof(DependenceOrder));
            if (this.IsSelfDetermined && dependenceOrder != 0)
                throw new InvalidOperationException(
                    $"Cannot set {nameof(DependenceOrder)} to a non-zero value for a self-determined user.");

            this.DependenceOrder = dependenceOrder;
        }

        public UserAcf(string fullName, string phone, int dependenceOrder, string? courseCodes)
            : this(fullName, phone, dependenceOrder)
        {
            // Accept empty course codes string in order to register the user
            // in all the courses of their school
            if (!string.IsNullOrWhiteSpace(courseCodes))
            {
                this.CourseCodes = courseCodes.
                    Split(',', StringSplitOptions.RemoveEmptyEntries).
                    Select(cc => short.Parse(cc.Trim(), CultureInfo.InvariantCulture)).
                    ToList();

                this.CourseCodesString = courseCodes;
            }
        }

        public User ToUser(int aspNetUserId)
        {
            return new()
            {
                AspNetUserId = aspNetUserId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DependenceOrder = this.DependenceOrder,
                IsSelfDetermined = this.IsSelfDetermined,
                HasAcceptedTerms = false
            };
        }

        public User ToUser(User userToUpdate, int aspNetUserId)
        {
            if (userToUpdate is null)
                throw new ArgumentNullException(nameof(userToUpdate));

            userToUpdate.AspNetUserId = aspNetUserId;
            userToUpdate.FirstName = this.FirstName;
            userToUpdate.LastName = this.LastName;
            userToUpdate.DependenceOrder = this.DependenceOrder;
            userToUpdate.IsSelfDetermined = this.IsSelfDetermined;

            return userToUpdate;
        }

        public string GenerateUserName(SchoolUnique schoolUq)
        {
            return $"{schoolUq}__P{this.PhoneString}__O{this.DependenceOrder}";
        }

        [JsonIgnore]
        public string FirstName { get; } = null!;

        [JsonIgnore]
        public string LastName { get; } = null!;

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; } = null!;

        [JsonProperty(PropertyName = "phone")]
        public string PhoneString { get; protected set; } = null!;

        [JsonProperty(PropertyName = "course_codes")]
        public string CourseCodesString { get; } = string.Empty;

        [JsonIgnore]
        public RoleRank Role { get; protected set; }

        [JsonIgnore]
        public bool IsSelfDetermined { get; protected set; }

        [JsonIgnore]
        public int DependenceOrder { get; set; }

        [JsonIgnore]
        public List<short> CourseCodes { get; }

        [JsonIgnore]
        public IEnumerable<IBotFeedback> BotFeedbacks { get; }

        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }

        [JsonIgnore]
        public IEnumerable<IGrade> Grades { get; }

        [JsonIgnore]
        public IEnumerable<IOneTimeCode> OneTimeCodes { get; }

        [JsonIgnore]
        public IEnumerable<IUserConnection> UserConnections { get; set; }


        [JsonIgnore]
        public IEnumerable<IUser> Children { get; }

        [JsonIgnore]
        public HashSet<Course> Courses { get; }

        IEnumerable<ICourse> IUser.Courses => this.Courses;

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; }

        [JsonIgnore]
        public IEnumerable<IUser> Parents { get; }

        [JsonIgnore]
        public HashSet<School> Schools { get; }

        IEnumerable<ISchool> IUser.Schools => this.Schools;
    }
}
