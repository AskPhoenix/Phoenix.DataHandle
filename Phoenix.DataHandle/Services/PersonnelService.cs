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

            var personnelPosts = (await this.GetAllPostsAsync());
            foreach (var personnelPost in personnelPosts)
            {
                if (!this.TryFindSchoolId(personnelPost, out int schoolId))
                    continue;

                PersonnelACF personnelACF = (PersonnelACF)(await WordPressClientWrapper.GetAcfAsync<PersonnelACF>(personnelPost.Id)).WithTitleCase();
                personnelACF.SchoolUnique = new SchoolUnique(personnelPost.GetTitle());

                var aspNetUser = await this.aspNetUserRepository.Find(checkUnique: personnelACF.MatchesUnique);
                if (aspNetUser is null)
                {
                    Logger.LogInformation($"Adding Personnel User with PhoneNumber: {aspNetUser.PhoneNumber}");

                    //TODO: Check if User needs to be created separately
                    //aspNetUser.User = personnelACF.ExtractUser();
                    aspNetUser = personnelACF.ToContext();
                    aspNetUserRepository.Create(aspNetUser, personnelACF.ExtractUser());
                    
                    this.aspNetUserRepository.Create(aspNetUser);
                    this.IdsLog.Add(aspNetUser.Id);

                    this.aspNetUserRepository.LinkSchool(aspNetUser, schoolId);
                }
                else
                {
                    Logger.LogInformation($"Updating Personnel User with PhoneNumber: {aspNetUser.PhoneNumber}");
                    this.aspNetUserRepository.Update(aspNetUser, personnelACF.ToContext(), personnelACF.ExtractUser());
                    this.IdsLog.Add(aspNetUser.Id);
                }

                Logger.LogInformation("Linking with the AspNetUserRoles of Personnel User");

                if (!aspNetUserRepository.HasRole(aspNetUser, personnelACF.RoleType))
                    aspNetUserRepository.LinkRole(aspNetUser, personnelACF.RoleType);
                
                aspNetUserRepository.DeleteRoles(aspNetUser, personnelACF.RoleType);

                Logger.LogInformation("Linking with the Courses of Personnel User");

                List<int> userCourseIds;
                if (string.IsNullOrEmpty(personnelACF.CourseCodesString) || personnelACF.RoleType.IsStaffAdmin())
                {
                    userCourseIds = this.SchoolRepository.FindCourses(schoolId).Select(c => c.Id).ToList();
                }
                else
                {
                    short[] courseCodes = personnelACF.ExtractCourseCodes();
                    userCourseIds = new List<int>(courseCodes.Length);
                    foreach (short courseCode in courseCodes)
                    {
                        if (!this.TryFindCourseId(personnelPost, out int courseId))
                            continue;
                        userCourseIds.Add(courseId);
                    }
                }

                aspNetUserRepository.LinkCourses(aspNetUser, userCourseIds);
            }

            Logger.LogInformation("Personnel synchronization finished");
            Logger.LogInformation("--------------------------------");
        }

    }
}
