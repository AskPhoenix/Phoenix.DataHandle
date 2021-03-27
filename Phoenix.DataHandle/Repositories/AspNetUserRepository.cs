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

        public override AspNetUsers Create(AspNetUsers tModel)
        {
            if (!string.IsNullOrEmpty(tModel.PhoneNumber))
                tModel.AffiliatedPhoneNumber = null;

            return base.Create(tModel);
        }

        public override AspNetUsers Update(AspNetUsers tModel)
        {
            if (!string.IsNullOrEmpty(tModel.PhoneNumber))
                tModel.AffiliatedPhoneNumber = null;

            return base.Update(tModel);
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
            tModel.AffiliatedPhoneNumber = tModelFrom.AffiliatedPhoneNumber;
            
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

        public bool AnyLogin(int userId, bool onlyActive = false)
        {
            var userLogins = this.dbContext.Set<AspNetUserLogins>().
                Where(l => l.UserId == userId);

            if (onlyActive)
                userLogins = userLogins.Where(l => l.IsActive);

            return userLogins.Any();
        }

        public bool AnyLogin(LoginProvider provider, string providerKey, bool onlyActive = false)
        {
            if (providerKey == null)
                throw new ArgumentNullException(nameof(providerKey));

            var userLogins = this.dbContext.Set<AspNetUserLogins>().
                Where(l => l.LoginProvider == provider.GetProviderName() && l.ProviderKey == providerKey);
            
            if (onlyActive)
                userLogins = userLogins.Where(l => l.IsActive);

            return userLogins.Any();
        }

        public void LinkLogin(AspNetUserLogins userLogin)
        {
            if (userLogin == null)
                throw new ArgumentNullException(nameof(userLogin));

            if (AnyLogin(userLogin.LoginProvider.ToLoginProvider(), userLogin.ProviderKey))
            {
                userLogin.UpdatedAt = DateTimeOffset.Now;
                this.dbContext.Set<AspNetUserLogins>().Update(userLogin);
            }
            else
            {
                userLogin.CreatedAt = DateTimeOffset.Now;
                this.dbContext.Set<AspNetUserLogins>().Add(userLogin);
            }

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

        public IQueryable<AspNetUsers> FindChildren(int parentId)
        {
            return this.dbContext.Set<Parenthood>().
                Include(p => p.Child).
                ThenInclude(ch => ch.User).
                Where(p => p.ParentId == parentId).
                Select(p => p.Child);
        }

        public IQueryable<AspNetUsers> FindParents(int childId)
        {
            return this.dbContext.Set<Parenthood>().
                Include(p => p.Parent).
                ThenInclude(p => p.User).
                Where(p => p.ChildId == childId).
                Select(p => p.Parent);
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

        public void LinkParenthood(AspNetUsers parent, AspNetUsers child)
        {
            this.dbContext.Set<Parenthood>().Add(new Parenthood { ParentId = parent.Id, ChildId = child.Id });
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
