using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class AspNetUserRepository : Repository<AspNetUsers>
    {
        private readonly int studentRoleId, teacherRoleId;

        public AspNetUserRepository(PhoenixContext dbContext) : base(dbContext) 
        {
            studentRoleId = this.dbContext.Set<AspNetRoles>().Single(r => r.Type == Role.Student).Id;
            teacherRoleId = this.dbContext.Set<AspNetRoles>().Single(r => r.Type == Role.Teacher).Id;
        }

        public AspNetUsers Create(AspNetUsers tModel, User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            var tore = this.Create(tModel);

            user.AspNetUserId = tore.Id;
            this.dbContext.Set<User>().Add(user);
            this.dbContext.SaveChanges();

            return tore;
        }

        public AspNetUsers Update(AspNetUsers tModel, AspNetUsers tModelFrom)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (tModelFrom == null)
                throw new ArgumentNullException(nameof(tModelFrom));

            tModel.UserName = tModelFrom.UserName;
            tModel.NormalizedUserName = tModelFrom.NormalizedUserName;
            tModel.PhoneNumber = tModelFrom.PhoneNumber;

            return this.Update(tModel);
        }

        public AspNetUsers Update(AspNetUsers tModel, AspNetUsers tModelFrom, User tModel2From)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (tModel2From == null)
                throw new ArgumentNullException(nameof(tModel2From));

            tModel.User.FirstName = tModel2From.FirstName;
            tModel.User.LastName = tModel2From.LastName;

            return this.Update(tModel, tModelFrom);
        }

        public bool HasRole(AspNetUsers user, Role role)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return this.dbContext.Set<AspNetUserRoles>().Include(ur => ur.Role).Any(ur => ur.UserId == user.Id && ur.Role.Type == role);
        }

        public bool AnyUserRole(int userId)
        {
            return this.dbContext.Set<AspNetUserRoles>().Any(ur => ur.UserId == userId);
        }

        public bool AnyUserRole(AspNetUsers user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return this.AnyUserRole(user.Id);
        }

        public bool AnyLogin(LoginProvider provider, string providerKey)
        {
            if (providerKey == null)
                throw new ArgumentNullException(nameof(providerKey));

            return this.dbContext.Set<AspNetUserLogins>().Any(l => l.LoginProvider == provider.GetProviderName() && l.ProviderKey == providerKey);
        }

        public void LinkLogin(AspNetUserLogins userLogin)
        {
            if (userLogin == null)
                throw new ArgumentNullException(nameof(userLogin));

            userLogin.CreatedAt = DateTimeOffset.Now;

            this.dbContext.Set<AspNetUserLogins>().Add(userLogin);
            this.dbContext.SaveChanges();
        }

        public AspNetUsers FindUserFromLogin(LoginProvider provider, string providerKey)
        {
            if (providerKey == null)
                throw new ArgumentNullException(nameof(providerKey));

            return this.dbContext.Set<AspNetUserLogins>().
                Include(l => l.User).
                ThenInclude(u => u.User).
                SingleOrDefault(l => l.LoginProvider == provider.GetProviderName() && l.ProviderKey == providerKey)?.
                User;
        }

        public IEnumerable<AspNetRoles> FindRoles(AspNetUsers tModel)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            return this.dbContext.Set<AspNetUserRoles>().
                Include(ur => ur.Role).
                Where(ur => ur.UserId == tModel.Id).
                Select(ur => ur.Role).
                AsEnumerable();
        }

        public void LinkSchool(UserSchool userSchool)
        {
            if (userSchool == null)
                throw new ArgumentNullException(nameof(userSchool));

            this.dbContext.Set<UserSchool>().Add(userSchool);
            this.dbContext.SaveChanges();
        }

        public void LinkRoles(AspNetUsers tModel, IEnumerable<int> roleIds)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (roleIds == null)
                throw new ArgumentNullException(nameof(roleIds));

            var aspNetUserRoles = roleIds.Select(id => new AspNetUserRoles() { RoleId = id, UserId = tModel.Id });
            this.dbContext.Set<AspNetUserRoles>().AddRange(aspNetUserRoles);
            this.dbContext.SaveChanges();
        }

        public void LinkRoles(AspNetUsers tModel, IEnumerable<Role> roles)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (roles == null)
                throw new ArgumentNullException(nameof(roles));

            var roleIds = this.dbContext.Set<AspNetRoles>().Where(r => roles.Contains(r.Type)).Select(r => r.Id);
            this.LinkRoles(tModel, roleIds);
        }

        public void LinkCourses(AspNetUsers tModel, IEnumerable<int> courseIds)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (courseIds == null)
                throw new ArgumentNullException(nameof(courseIds));

            if (this.dbContext.Set<AspNetUserRoles>().Any(ur => ur.UserId == tModel.Id && ur.RoleId == studentRoleId))
            {
                var idsToExclude = this.dbContext.Set<StudentCourse>()
                    .Where(sc => sc.StudentId == tModel.Id && courseIds.Contains(sc.CourseId))
                    .Select(sc => sc.CourseId);

                var idsToKeep = courseIds.ToList();
                idsToKeep.RemoveAll(id => idsToExclude.Contains(id));

                var studentCourses = idsToKeep.Select(id => new StudentCourse() { StudentId = tModel.Id, CourseId = id });

                this.dbContext.Set<StudentCourse>().AddRange(studentCourses);
                this.dbContext.SaveChanges();
            }

            if (this.dbContext.Set<AspNetUserRoles>().Any(ur => ur.UserId == tModel.Id && ur.RoleId >= teacherRoleId))
            {
                var idsToExclude = this.dbContext.Set<TeacherCourse>()
                    .Where(tc => tc.TeacherId == tModel.Id && courseIds.Contains(tc.CourseId))
                    .Select(tc => tc.CourseId);

                var idsToKeep = courseIds.ToList();
                idsToKeep.RemoveAll(id => idsToExclude.Contains(id));

                var teacherCourses = idsToKeep.Select(id => new TeacherCourse() { TeacherId = tModel.Id, CourseId = id });

                this.dbContext.Set<TeacherCourse>().AddRange(teacherCourses);
                this.dbContext.SaveChanges();
            }
        }
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
