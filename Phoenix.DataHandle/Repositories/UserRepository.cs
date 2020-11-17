﻿using System.Linq;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(PhoenixContext dbContext) : base(dbContext) { }

        public User Update(User tModel, User tModelFrom)
        {
            tModel.FirstName = tModelFrom.FirstName;
            tModel.LastName = tModelFrom.LastName;
            
            return base.Update(tModel);
        }

        public void LinkSchool(UserSchool userSchool)
        {
            this.dbContext.Set<UserSchool>().Add(userSchool);
        }
    }

    public static class UserRepositoryExtensions
    {
        public static IQueryable<User> FilterAdmins(this IQueryable<User> users)
        {
            return users.Where(a => a.AspNetUser.AspNetUserRoles.Any(b => b.Role.Type == Role.Admin || b.Role.Type == Role.SuperAdmin));
        }

        public static IQueryable<User> FilterOwners(this IQueryable<User> users)
        {
            return users.Where(a => a.AspNetUser.AspNetUserRoles.Any(b => b.Role.Type == Role.SchoolOwner));
        }

        public static IQueryable<User> FilterTeachers(this IQueryable<User> users)
        {
            return users.Where(a => a.AspNetUser.AspNetUserRoles.Any(b => b.Role.Type == Role.Teacher));
        }

        public static IQueryable<User> FilterStudents(this IQueryable<User> users)
        {
            return users.Where(a => a.AspNetUser.AspNetUserRoles.Any(b => b.Role.Type == Role.Student));
        }
    }
}
