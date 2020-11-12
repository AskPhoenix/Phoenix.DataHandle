using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(PhoenixContext dbContext) : base(dbContext)
        {
        }
        
    }

    public static class UserRepositoryExtensions
    {
        public static IQueryable<User> filterAdmins(this IQueryable<User> users)
        {
            return users.Where(a => a.AspNetUser.AspNetUserRoles.Any(b => b.Role.Type == Role.Admin || b.Role.Type == Role.SuperAdmin));
        }

        public static IQueryable<User> filterOwners(this IQueryable<User> users)
        {
            return users.Where(a => a.AspNetUser.AspNetUserRoles.Any(b => b.Role.Type == Role.SchoolOwner));
        }

        public static IQueryable<User> filterTeachers(this IQueryable<User> users)
        {
            return users.Where(a => a.AspNetUser.AspNetUserRoles.Any(b => b.Role.Type == Role.Teacher));
        }

        public static IQueryable<User> filterStudents(this IQueryable<User> users)
        {
            return users.Where(a => a.AspNetUser.AspNetUserRoles.Any(b => b.Role.Type == Role.Student));
        }

    }

}
