using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUser : IdentityUser<int>, IAspNetUsers
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset RegisteredAt => this.CreatedAt;
        public ApplicationType CreatedApplicationType { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ApplicationUserLogin> ApplicationUserLogin { get; set; }

        IUser IAspNetUsers.User => this.User;
        IEnumerable<IAspNetUserLogins> IAspNetUsers.AspNetUserLogins => this.ApplicationUserLogin;
        IEnumerable<IAspNetUserRoles> IAspNetUsers.Roles { get; }
        IEnumerable<IAttendance> IAspNetUsers.Attendances { get; }
        IEnumerable<IParenthood> IAspNetUsers.Children { get; }
        IEnumerable<IParenthood> IAspNetUsers.Parents { get; }
        IEnumerable<IStudentCourse> IAspNetUsers.StudentCourses { get; }
        IEnumerable<IStudentExam> IAspNetUsers.StudentExams { get; }
        IEnumerable<IStudentExercise> IAspNetUsers.StudentExercises { get; }
        IEnumerable<ITeacherCourse> IAspNetUsers.TeacherCourses { get; }
        IEnumerable<IUserSchool> IAspNetUsers.UserSchools { get; }
        

        public ApplicationUser() { }

        public ApplicationUser(IdentityUser<int> identity) : this()
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            this.Id = identity.Id;
            this.AccessFailedCount = identity.AccessFailedCount;
            this.ConcurrencyStamp = identity.ConcurrencyStamp;
            this.Email = identity.Email;
            this.EmailConfirmed = identity.EmailConfirmed;
            this.LockoutEnabled = identity.LockoutEnabled;
            this.LockoutEnd = identity.LockoutEnd;
            this.NormalizedEmail = identity.NormalizedEmail;
            this.NormalizedUserName = identity.NormalizedUserName;
            this.PasswordHash = identity.PasswordHash;
            this.SecurityStamp = identity.SecurityStamp;
            this.PhoneNumber = identity.PhoneNumber;
            this.PhoneNumberConfirmed = identity.PhoneNumberConfirmed;
            this.UserName = identity.UserName;
            this.TwoFactorEnabled = identity.TwoFactorEnabled;
        }
    }
}