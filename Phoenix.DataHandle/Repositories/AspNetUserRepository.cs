using System.Linq;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class AspNetUserRepository : Repository<AspNetUsers>
    {
        public AspNetUserRepository(PhoenixContext dbContext) : base(dbContext) { }
    }

    public static class AspNetUserRepositoryExtensions
    {
        public static IQueryable<AspNetUsers> FilterAdmins(this IQueryable<AspNetUsers> users)
        {
            return users.Where(a => a.AspNetUserRoles.Any(b => b.Role.Type == Role.Admin || b.Role.Type == Role.SuperAdmin));
        }

        public static IQueryable<AspNetUsers> FilterOwners(this IQueryable<AspNetUsers> users)
        {
            return users.Where(a => a.AspNetUserRoles.Any(b => b.Role.Type == Role.SchoolOwner));
        }

        public static IQueryable<AspNetUsers> FilterTeachers(this IQueryable<AspNetUsers> users)
        {
            return users.Where(a => a.AspNetUserRoles.Any(b => b.Role.Type == Role.Teacher));
        }

        public static IQueryable<AspNetUsers> FilterStudents(this IQueryable<AspNetUsers> users)
        {
            return users.Where(a => a.AspNetUserRoles.Any(b => b.Role.Type == Role.Student));
        }
    }
}
