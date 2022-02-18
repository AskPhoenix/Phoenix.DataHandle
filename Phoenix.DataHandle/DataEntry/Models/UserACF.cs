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
        public virtual string FullName { get; }
        public virtual string CourseCodesString { get; }

        public abstract string PhoneNumber { get; }
        public List<short> CourseCodes { get; }

        public string FirstName { get; }
        public string LastName { get; }

        public UserACF(string fullName, string courseCodesString)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));
            if (string.IsNullOrWhiteSpace(courseCodesString))
                throw new ArgumentNullException(nameof(courseCodesString));

            this.FullName = fullName.ToTitleCase();
            this.CourseCodesString = courseCodesString;

            this.FirstName = GetFirstName(this.FullName);
            this.LastName = GetLastName(this.FullName);

            this.CourseCodes = courseCodesString.
                Split(',', StringSplitOptions.RemoveEmptyEntries).
                Select(cc => short.Parse(cc.Trim(), CultureInfo.InvariantCulture)).
                ToList();
        }

        public Expression<Func<AspNetUser, bool>> GetUniqueExpression() => u =>
            u.PhoneNumber == this.PhoneNumber && u.User.IsSelfDetermined;

        protected static string GetFirstName(string fullName, bool truncate = true)
        {
            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            string tore = string.Join(' ', names.Take((int)Math.Ceiling(names.Length / 2.0)));

            if (truncate)
                tore = tore.Truncate(255);

            return tore;
        }

        protected static string GetLastName(string fullName, bool truncate = true)
        {
            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (names.Length == 1)
                return string.Empty;

            string tore = string.Join(' ', names.TakeLast(names.Length / 2));

            if (truncate)
                tore = tore.Truncate(255);

            return tore;
        }

        public string GetUserName(int schoolId, int dependanceNum) // Dependance number separates non-self-dependent users. The phone owner takes a value of 0.
        {
            var firstNameSub = this.FirstName[..Math.Min(3, this.FirstName.Length)];

            return "S" + schoolId + "_P" + this.PhoneNumber + "_N" + firstNameSub + "." + this.LastName + "_O" + dependanceNum;
        }
    }
}
