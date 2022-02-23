﻿using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetUser
    {
        string UserName { get; set; }
        string? Email { get; set; }
        string PhoneNumber { get; set; }

        IUser User { get; }
        IEnumerable<IAspNetUserLogin> AspNetUserLogins { get; }
        IEnumerable<IBotFeedback> BotFeedbacks { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        
        IEnumerable<IAspNetUser> Children { get; }
        IEnumerable<ICourse> Courses { get; }
        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<IAspNetUser> Parents { get; }
        IEnumerable<IAspNetRole> Roles { get; }
        IEnumerable<ISchool> Schools { get; }
    }
}