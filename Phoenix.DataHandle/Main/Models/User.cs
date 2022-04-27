using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class User
    {
        public User()
        {
            BotFeedbacks = new HashSet<BotFeedback>();
            Broadcasts = new HashSet<Broadcast>();
            Grades = new HashSet<Grade>();
            OneTimeCodes = new HashSet<OneTimeCode>();
            UserLogins = new HashSet<UserLogin>();
            Children = new HashSet<User>();
            Courses = new HashSet<Course>();
            Lectures = new HashSet<Lecture>();
            Parents = new HashSet<User>();
            //Roles = new HashSet<Role>();
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
        public string PhoneCountryCode { get; set; } = null!;
        public bool PhoneNumberConfirmed { get; set; }
        public int DependenceOrder { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ObviatedAt { get; set; }
        
        public virtual UserInfo UserInfo { get; set; } = null!;
        public virtual ICollection<BotFeedback> BotFeedbacks { get; set; }
        public virtual ICollection<Broadcast> Broadcasts { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<OneTimeCode> OneTimeCodes { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }

        public virtual ICollection<User> Children { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<User> Parents { get; set; }
        //public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}
