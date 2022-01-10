﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class SchoolRepository : ObviableRepository<School>
    {
        public SchoolRepository(PhoenixContext dbContext) : base(dbContext) { }

        // TODO: Remove and use method: TModel Update(TModel model)
        public School Update(School tModel, School tModelFrom)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (tModelFrom == null)
                throw new ArgumentNullException(nameof(tModelFrom));

            tModel.Name = tModelFrom.Name;
            tModel.Slug = tModelFrom.Slug;
            tModel.City = tModelFrom.City;
            tModel.AddressLine = tModelFrom.AddressLine;
            tModel.Info = tModelFrom.Info;

            //The columns of the unique keys should not be copied

            if (!string.IsNullOrWhiteSpace(tModelFrom.FacebookPageId))
                tModel.FacebookPageId = tModelFrom.FacebookPageId;

            return this.Update(tModel);
        }

        public School Update(School tModel, School tModelFrom, SchoolSettings tModel2From)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (tModel2From == null)
                throw new ArgumentNullException(nameof(tModel2From));

            tModel.SchoolSettings.Language = tModel2From.Language;
            tModel.SchoolSettings.Locale2 = tModel2From.Locale2;
            tModel.SchoolSettings.TimeZone = tModel2From.TimeZone;

            return this.Update(tModel, tModelFrom);
        }

        public IQueryable<Course> FindCourses(int id)
        {
            this.Include(a => a.Course);
            return this.Find().Where(a => a.Id == id).SelectMany(a => a.Course);
        }

        public IQueryable<Classroom> FindClassrooms(int id)
        {
            this.Include(a => a.Classroom);
            return this.Find().Where(a => a.Id == id).SelectMany(a => a.Classroom);
        }

        public IQueryable<AspNetUsers> FindUsers(int id)
        {
            return this.Find()
                .Include(a => a.UserSchool)
                .ThenInclude(a => a.AspNetUser)
                .Where(a => a.Id == id)
                .SelectMany(a => a.UserSchool)
                .Select(a => a.AspNetUser);
        }

        public IQueryable<AspNetUsers> FindPersonnel(int id)
        {
            var personnelRoles = RoleExtensions.GetPersonnelRoles();
            return this.FindUsers(id)
                .Where(u => u.AspNetUserRoles.Any(ur => personnelRoles.Contains(ur.Role.Type)));
        }

        public IQueryable<AspNetUsers> FindClients(int id)
        {
            var clientRoles = RoleExtensions.GetClientRoles();
            return this.FindUsers(id)
                .Where(u => u.AspNetUserRoles.Any(ur => clientRoles.Contains(ur.Role.Type)));
        }

        public SchoolSettings FindSchoolSettings(int id)
        {
            return this.dbContext.Set<SchoolSettings>().Single(a => a.SchoolId == id);
        }
    }
}
