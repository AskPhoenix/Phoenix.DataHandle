using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            BotFeedbacks = new HashSet<BotFeedback>();
            Broadcasts = new HashSet<Broadcast>();
            Grades = new HashSet<Grade>();
            OneTimeCodes = new HashSet<OneTimeCode>();
            UserConnections = new HashSet<UserConnection>();
            Children = new HashSet<UserInfo>();
            Courses = new HashSet<Course>();
            Lectures = new HashSet<Lecture>();
            Parents = new HashSet<UserInfo>();
            Schools = new HashSet<School>();
        }

        public int AspNetUserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsSelfDetermined { get; set; }
        public int DependenceOrder { get; set; }
        public bool HasAcceptedTerms { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ObviatedAt { get; set; }

        public virtual ICollection<BotFeedback> BotFeedbacks { get; set; }
        public virtual ICollection<Broadcast> Broadcasts { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<OneTimeCode> OneTimeCodes { get; set; }
        public virtual ICollection<UserConnection> UserConnections { get; set; }

        public virtual ICollection<UserInfo> Children { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<UserInfo> Parents { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}
