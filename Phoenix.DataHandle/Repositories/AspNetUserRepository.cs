﻿using System;
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
            tModel.CreatedAt = DateTimeOffset.Now;

            return base.Create(tModel);
        }

        public override AspNetUsers Update(AspNetUsers tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }

        public AspNetUsers Update(AspNetUsers tModel, AspNetUsers tModelFrom)
        {
            tModel.UserName = tModelFrom.UserName;
            tModel.NormalizedUserName = tModelFrom.NormalizedUserName;
            tModel.PhoneNumber = tModelFrom.PhoneNumber;

            return this.Update(tModel);
        }

        public bool HasRole(AspNetUsers user, Role role)
        {
            return this.dbContext.Set<AspNetUserRoles>().Include(ur => ur.Role).Any(ur => ur.UserId == user.Id && ur.Role.Type == role);
        }

        public void LinkRoles(AspNetUsers tModel, IEnumerable<int> roleIds)
        {
            var aspNetUserRoles = roleIds.Select(id => new AspNetUserRoles() { RoleId = id, UserId = tModel.Id });

            this.dbContext.Set<AspNetUserRoles>().AddRange(aspNetUserRoles);
        }

        public void LinkRoles(AspNetUsers tModel, IEnumerable<Role> roles)
        {
            var roleIds = this.dbContext.Set<AspNetRoles>().Where(r => roles.Contains(r.Type)).Select(r => r.Id);

            this.LinkRoles(tModel, roleIds);
        }

        public void LinkCourses(AspNetUsers tModel, IEnumerable<int> courseIds)
        {
            if (this.dbContext.Set<AspNetUserRoles>().Any(ur => ur.UserId == tModel.Id && ur.RoleId == studentRoleId))
            {
                var idsToExclude = this.dbContext.Set<StudentCourse>()
                    .Where(sc => sc.StudentId == tModel.Id && courseIds.Contains(sc.CourseId))
                    .Select(sc => sc.CourseId);

                var idsToKeep = courseIds.ToList();
                idsToKeep.RemoveAll(id => idsToExclude.Contains(id));

                var studentCourses = idsToKeep.Select(id => new StudentCourse() { StudentId = tModel.Id, CourseId = id });

                this.dbContext.Set<StudentCourse>().AddRange(studentCourses);
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
