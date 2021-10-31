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
        public AspNetUserRepository(PhoenixContext dbContext) 
            : base(dbContext) { }

        //TODO: Use Repository's Include method
        //TODO: Revise all repositories methods
        public AspNetUsers Update(AspNetUsers tModel, AspNetUsers tModelFrom)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (tModelFrom == null)
                throw new ArgumentNullException(nameof(tModelFrom));

            tModel.UserName = tModelFrom.UserName;
            tModel.NormalizedUserName = tModelFrom.NormalizedUserName;
            tModel.PhoneNumber = tModelFrom.PhoneNumber;
            
            tModel.AccessFailedCount = tModelFrom.AccessFailedCount;
            tModel.CreatedApplicationType = tModelFrom.CreatedApplicationType;
            tModel.Email = tModelFrom.Email;
            tModel.EmailConfirmed = tModelFrom.EmailConfirmed;
            tModel.LockoutEnabled = tModelFrom.LockoutEnabled;
            tModel.LockoutEnd = tModelFrom.LockoutEnd;
            tModel.NormalizedEmail = tModelFrom.NormalizedEmail;
            tModel.PhoneNumberConfirmed = tModelFrom.PhoneNumberConfirmed;
            tModel.TwoFactorEnabled = tModelFrom.TwoFactorEnabled;

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

        public IQueryable<AspNetUsers> FindTeachersForCourse(int courseId)
        {
            return this.dbContext.Set<AspNetUsers>().
                Where(u => u.TeacherCourse.Any(tc => tc.CourseId == courseId));
        }

        public IQueryable<AspNetUsers> FindStudentsForCourse(int courseId)
        {
            return this.dbContext.Set<AspNetUsers>().
                Where(u => u.StudentCourse.Any(sc => sc.CourseId == courseId));
        }

        public IQueryable<AspNetUsers> FindAllForCourse(int courseId)
        {
            var teachers = this.FindTeachersForCourse(courseId);
            var students = this.FindStudentsForCourse(courseId);

            return teachers.Concat(students);
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

        public bool AnyAffiliatedUsers(int userId)
        {
            return this.dbContext.Set<Parenthood>().
                Where(p => p.ParentId == userId).
                Any();
        }

        public bool HasLogin(LoginProvider provider, string providerKey, bool onlyActive = false)
        {
            var login = this.FindLogin(provider, providerKey);

            bool hasLogin = login != null;
            if (onlyActive && hasLogin)
                hasLogin &= login.IsActive;

            return hasLogin;
        }

        public bool AnyLogin(int userId, bool onlyActive = false)
        {
            var userLogins = this.dbContext.Set<AspNetUserLogins>().
                Where(l => l.UserId == userId);

            if (onlyActive)
                userLogins = userLogins.Where(l => l.IsActive);

            return userLogins.Any();
        }

        public bool AnyLogin(int userId, LoginProvider provider, bool onlyActive = false)
        {
            var userLogins = this.dbContext.Set<AspNetUserLogins>().
                Where(l => l.UserId == userId && l.LoginProvider == provider.GetProviderName());
            
            if (onlyActive)
                userLogins = userLogins.Where(l => l.IsActive);

            return userLogins.Any();
        }

        public void LinkLogin(LoginProvider provider, string providerKey, int userId, bool activate)
        {
            if (provider == LoginProvider.Other)
                throw new InvalidOperationException("Provider needs to be a valid channel");
            if (string.IsNullOrEmpty(providerKey))
                throw new ArgumentNullException(nameof(providerKey));

            var login = this.FindLogin(provider, providerKey);

            if (login is null)
            {
                login = new AspNetUserLogins()
                {
                    LoginProvider = provider.GetProviderName(),
                    ProviderDisplayName = provider.GetProviderName().ToLower(),
                    ProviderKey = providerKey,
                    IsActive = activate,
                    UserId = userId,
                    CreatedAt = DateTimeOffset.UtcNow
                };
                this.dbContext.Set<AspNetUserLogins>().Add(login);
            }
            else
            {
                login.UserId = userId;
                login.IsActive = activate;
                login.UpdatedAt = DateTimeOffset.UtcNow;

                this.dbContext.Set<AspNetUserLogins>().Update(login);
            }

            this.dbContext.SaveChanges();
        }

        public IEnumerable<AspNetUserLogins> Logout(int userId, bool logoutAffiliatedUsers = true)
        {
            List<int> userIdsToLogout = new List<int> { userId };
            
            if (logoutAffiliatedUsers)
                userIdsToLogout.AddRange(this.FindChildren(userId).Select(c => c.Id));

            var logins = this.dbContext.Set<AspNetUserLogins>().Where(l => userIdsToLogout.Contains(l.UserId));
            foreach (var login in logins)
            {
                login.IsActive = false;
                login.UpdatedAt = DateTimeOffset.UtcNow;
            }

            this.dbContext.Set<AspNetUserLogins>().UpdateRange(logins);
            this.dbContext.SaveChanges();

            return logins.AsEnumerable();
        }

        public AspNetUserLogins FindLogin(LoginProvider provider, string providerKey)
        {
            return this.dbContext.Set<AspNetUserLogins>().
                SingleOrDefault(l => l.LoginProvider == provider.GetProviderName() && l.ProviderKey == providerKey);
        }

        //TODO: Check only the Active Logins
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

        public IQueryable<AspNetUsers> FindChildren(int parentId)
        {
            return this.dbContext.Set<Parenthood>().
                Include(p => p.Child).
                ThenInclude(ch => ch.User).
                Where(p => p.ParentId == parentId).
                Select(p => p.Child);
        }

        public AspNetUsers FindChild(int parentId, string childFirstName, string childLastName)
        {
            return this.FindChildren(parentId).
                AsEnumerable().
                SingleOrDefault(c => c.User.FirstName == childFirstName && c.User.LastName == childLastName);
        }

        public IQueryable<AspNetUsers> FindParents(int childId)
        {
            return this.dbContext.Set<Parenthood>().
                Include(p => p.Parent).
                ThenInclude(p => p.User).
                Where(p => p.ChildId == childId).
                Select(p => p.Parent);
        }

        public IQueryable<AspNetUsers> FindParents(IList<int> childIds)
        {
            if (childIds is null)
                throw new ArgumentNullException(nameof(childIds));

            IQueryable<AspNetUsers> parents = FindParents(childIds.First());
            foreach (int childId in childIds.Skip(1))
                parents = parents.Concat(FindParents(childId));

            return parents;
        }

        public void LinkSchool(UserSchool userSchool)
        {
            if (userSchool == null)
                throw new ArgumentNullException(nameof(userSchool));

            this.dbContext.Set<UserSchool>().Add(userSchool);
            this.dbContext.SaveChanges();
        }

        public void LinkSchool(AspNetUsers tModel, int schoolId)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            var userSchool = new UserSchool
            {
                AspNetUserId = tModel.Id,
                SchoolId = schoolId,
                EnrolledOn = DateTimeOffset.UtcNow
            };

            this.LinkSchool(userSchool);
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

        public void LinkRole(AspNetUsers tModel, Role roleType)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            var roleIds = this.dbContext.Set<AspNetRoles>().Where(r => r.Type == roleType).Select(r => r.Id);
            this.LinkRoles(tModel, roleIds);
        }

        public void LinkCourses(AspNetUsers tModel, IList<int> courseIds, bool deleteAdditionalLinks = false)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (courseIds == null)
                throw new ArgumentNullException(nameof(courseIds));

            var aspNetUserRolesSet = this.dbContext.Set<AspNetUserRoles>();
            var backendRoles = RoleExtensions.GetBackendRoles();
            var staffRoles = RoleExtensions.GetStaffRoles();

            if (aspNetUserRolesSet.Any(ur => ur.UserId == tModel.Id && (ur.Role.Type == Role.Student || backendRoles.Contains(ur.Role.Type))))
            {
                var studentCourseSet = this.dbContext.Set<StudentCourse>();
                var studentCourses = studentCourseSet.Where(sc => sc.StudentId == tModel.Id);

                var idsToExclude = studentCourses.
                    Where(sc => courseIds.Contains(sc.CourseId)).
                    Select(sc => sc.CourseId);

                var idsToKeep = courseIds.ToList();
                idsToKeep.RemoveAll(id => idsToExclude.Contains(id));

                var studentCoursesToAdd = idsToKeep.Select(id => new StudentCourse() { StudentId = tModel.Id, CourseId = id });

                studentCourseSet.AddRange(studentCoursesToAdd);

                if (deleteAdditionalLinks)
                {
                    var additionalCourses = studentCourses.
                        Where(sc => !courseIds.Contains(sc.CourseId));
                    if (additionalCourses.Any())
                        studentCourseSet.RemoveRange(additionalCourses);
                }

                this.dbContext.SaveChanges();
            }
            
            if (aspNetUserRolesSet.Any(ur => ur.UserId == tModel.Id && (staffRoles.Contains(ur.Role.Type) || backendRoles.Contains(ur.Role.Type))))
            {
                var teacherCourseSet = this.dbContext.Set<TeacherCourse>();
                var teacherCourses = teacherCourseSet.Where(sc => sc.TeacherId == tModel.Id);

                var idsToExclude = teacherCourses.
                    Where(tc => courseIds.Contains(tc.CourseId)).
                    Select(tc => tc.CourseId);

                var idsToKeep = courseIds.ToList();
                idsToKeep.RemoveAll(id => idsToExclude.Contains(id));

                var teacherCoursesToAdd = idsToKeep.Select(id => new TeacherCourse() { TeacherId = tModel.Id, CourseId = id });

                teacherCourseSet.AddRange(teacherCoursesToAdd);

                if (deleteAdditionalLinks)
                {
                    var additionalCourses = teacherCourses.
                        Where(sc => !courseIds.Contains(sc.CourseId));
                    if (additionalCourses.Any())
                        teacherCourseSet.RemoveRange(additionalCourses);
                }

                this.dbContext.SaveChanges();
            }
        }

        public void LinkParenthood(int parentId, int childId)
        {
            this.dbContext.Set<Parenthood>().Add(new Parenthood { ParentId = parentId, ChildId = childId });
            this.dbContext.SaveChanges();
        }

        public void DeleteRoles(AspNetUsers tModel, IList<Role> rolesTypeToKeep = null)
        {
            var rolesToDelete = this.FindRoles(tModel);
            if (rolesTypeToKeep != null)
                rolesToDelete = rolesToDelete.Where(r => !rolesTypeToKeep.Contains(r.Type));
            
            if (!rolesToDelete.Any())
                return;
            
            this.dbContext.Set<AspNetUserRoles>()
                .RemoveRange(rolesToDelete.Select(r => new AspNetUserRoles { RoleId = r.Id, UserId = tModel.Id }));
            
            this.dbContext.SaveChanges();
        }
    }

    public static class AspNetUserRepositoryExtensions
    {
        public static IQueryable<AspNetUsers> FilterAdmins(this IQueryable<AspNetUsers> users)
        {
            return users.Where(a => a.AspNetUserRoles.Any(b => b.Role.Type == Role.SchoolAdmin || b.Role.Type == Role.SuperAdmin));
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
