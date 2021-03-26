using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.WordPress;
using Phoenix.DataHandle.WordPress.Models;
using Phoenix.DataHandle.WordPress.Models.Uniques;
using Phoenix.DataHandle.WordPress.Utilities;
using Phoenix.DataHandle.WordPress.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Services
{
    public class PersonnelService : WPService
    {
        private readonly AspNetUserRepository aspNetUserRepository;

        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.Personnel);

        public PersonnelService(PhoenixContext phoenixContext, ILogger<WPService> logger,
            string specificSchoolUnique = null, bool deleteAdditional = false) 
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional)
        {
            this.aspNetUserRepository = new AspNetUserRepository(phoenixContext);
            this.aspNetUserRepository.Include(u => u.User);
        }

        public override void DeleteComplement()
        {
            throw new NotImplementedException();
        }

        public override async Task SynchronizeAsync()
        {
            Logger.LogInformation("Personnel synchronization started");

            var personnelPosts = await this.GetAllPostsAsync();
            foreach (var personnelPost in personnelPosts)
            {
                if (!this.TryFindSchool(personnelPost, out School school))
                    continue;

                PersonnelACF personnelAcf = (PersonnelACF)(await WordPressClientWrapper.GetAcfAsync<PersonnelACF>(personnelPost.Id)).WithTitleCase();
                personnelAcf.SchoolUnique = new SchoolUnique(personnelPost.GetTitle());

                var aspNetUser = await this.aspNetUserRepository.Find(checkUnique: personnelAcf.MatchesUnique);
                if (aspNetUser is null)
                {
                    Logger.LogInformation($"Adding Personnel User with PhoneNumber: {aspNetUser.PhoneNumber}");

                    aspNetUser = personnelAcf.ToContext();
                    aspNetUser.User = personnelAcf.ExtractUser();
                    aspNetUserRepository.Create(aspNetUser);
                    
                    this.aspNetUserRepository.Create(aspNetUser);
                    this.IdsLog.Add(aspNetUser.Id);

                    this.aspNetUserRepository.LinkSchool(aspNetUser, school.Id);
                }
                else
                {
                    Logger.LogInformation($"Updating Personnel User with PhoneNumber: {aspNetUser.PhoneNumber}");
                    this.aspNetUserRepository.Update(aspNetUser, personnelAcf.ToContext(), personnelAcf.ExtractUser());
                    this.IdsLog.Add(aspNetUser.Id);
                }

                Logger.LogInformation("Linking with the AspNetUserRoles of Personnel User");

                if (!aspNetUserRepository.HasRole(aspNetUser, personnelAcf.RoleType))
                    aspNetUserRepository.LinkRole(aspNetUser, personnelAcf.RoleType);
                
                aspNetUserRepository.DeleteRoles(aspNetUser, personnelAcf.RoleType);

                Logger.LogInformation("Linking with the Courses of Personnel User");

                List<int> userCourseIds;
                if (string.IsNullOrEmpty(personnelAcf.CourseCodesString) || personnelAcf.RoleType.IsStaffAdmin())
                {
                    userCourseIds = this.SchoolRepository.FindCourses(school.Id).Select(c => c.Id).ToList();
                }
                else
                {
                    short[] courseCodes = personnelAcf.ExtractCourseCodes();
                    userCourseIds = new List<int>(courseCodes.Length);
                    foreach (short courseCode in courseCodes)
                    {
                        if (!this.TryFindCourse(personnelPost, school.Id, out Course course))
                            continue;
                        userCourseIds.Add(course.Id);
                    }
                }

                aspNetUserRepository.LinkCourses(aspNetUser, userCourseIds);
            }

            Logger.LogInformation("Personnel synchronization finished");
            Logger.LogInformation("--------------------------------");
        }

    }
}
