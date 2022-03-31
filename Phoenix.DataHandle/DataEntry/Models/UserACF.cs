using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public abstract class UserACF
    {
        // TODO: Check if JsonProperty names are inherited to children classes
        [JsonProperty(PropertyName = "full_name")]
        public virtual string FullName { get; }

        [JsonProperty(PropertyName = "course_codes")]
        public virtual string CourseCodesString { get; }

        [JsonProperty(PropertyName = "phone")]
        public abstract string PhoneNumber { get; }

        [JsonProperty(PropertyName = "course_codes_list")]
        public List<short> CourseCodes { get; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; }
        
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; }

        [JsonProperty(PropertyName = "dependance_order")]
        public int? DependanceOrder { get; set; }

        [JsonProperty(PropertyName = "user")]
        public IUser User { get; }


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

        [JsonIgnore]
        public IEnumerable<ICourse> Courses { get; }

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; }

        [JsonIgnore]
        public IEnumerable<IAspNetUser> Parents { get; }

        [JsonIgnore]
        public IEnumerable<IAspNetRole> Roles { get; }

        [JsonIgnore]
        public IEnumerable<ISchool> Schools { get; }

        public UserACF(string fullName, string courseCodesString)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));
            if (string.IsNullOrWhiteSpace(courseCodesString))
                throw new ArgumentNullException(nameof(courseCodesString));

            this.FullName = fullName.ToTitleCase();
            this.CourseCodesString = courseCodesString;

            this.FirstName = GetName(this.FullName, selFirstName: true);
            this.LastName = GetName(this.FullName, selFirstName: false);

            this.CourseCodes = courseCodesString.
                Split(',', StringSplitOptions.RemoveEmptyEntries).
                Select(cc => short.Parse(cc.Trim(), CultureInfo.InvariantCulture)).
                ToList();

            this.User = new User
            {
                FirstName = this.FirstName,
                LastName = this.LastName
            };
        }

        public Expression<Func<AspNetUser, bool>> GetUniqueExpression(int dependanceOrder) => u =>
            u.PhoneNumber == this.PhoneNumber && u.PhoneNumberDependanceOrder == dependanceOrder;

        // TODO: Check if this can be translated to SQL query. If yes, delete the other GetUniqueExpression method
        public Expression<Func<AspNetUser, bool>> GetUniqueExpression()
        {
            if (this.DependanceOrder is null)
                throw new InvalidOperationException("");

            return GetUniqueExpression(this.DependanceOrder.Value);
        }

        protected static string GetName(string fullName, bool selFirstName, bool truncate = true)
        {
            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (!selFirstName && names.Length == 1)
                return string.Empty;

            string tore;
            if (selFirstName)
                tore = string.Join(' ', names.Take((int)Math.Ceiling(names.Length / 2.0)));
            else
                tore = string.Join(' ', names.TakeLast(names.Length / 2));

            if (truncate)
                tore = tore.Truncate(255);

            return tore;
        }

        // Dependance order separates non-self-dependent users. The phone owner takes a value of 0.
        public string GetUserName(int schoolCode)
        {
            if (this.DependanceOrder is null)
                throw new InvalidOperationException("User's dependance order number must be set first.");

            return "S" + schoolCode + "_P" + this.PhoneNumber + "_N" + this.FirstName + "_O" + this.DependanceOrder;
        }
    }
}
