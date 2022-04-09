using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            BotFeedbacks = new HashSet<BotFeedback>();
            Broadcasts = new HashSet<Broadcast>();
            Grades = new HashSet<Grade>();
            Children = new HashSet<AspNetUser>();
            Courses = new HashSet<Course>();
            Lectures = new HashSet<Lecture>();
            Parents = new HashSet<AspNetUser>();
            Roles = new HashSet<AspNetRole>();
            Schools = new HashSet<School>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string NormalizedUserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public bool PhoneNumberConfirmed { get; set; }
        public string? PhoneNumberVerificationCode { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public ApplicationType CreatedApplicationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ObviatedAt { get; set; }
        public int PhoneNumberDependanceOrder { get; set; }
        public DateTime? PhoneNumberVerificationCodeCreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<BotFeedback> BotFeedbacks { get; set; }
        public virtual ICollection<Broadcast> Broadcasts { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }

        public virtual ICollection<AspNetUser> Children { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<AspNetUser> Parents { get; set; }
        public virtual ICollection<AspNetRole> Roles { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}
