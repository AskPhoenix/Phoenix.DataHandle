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
            string specificSchoolUnique = null, bool deleteAdditional = false, bool quiet = false) 
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional, quiet)
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
                    if (!Quiet)
                        Logger.LogInformation($"Adding Personnel User with PhoneNumber: {personnelAcf.PhoneString}");

                    aspNetUser = personnelAcf.ToContext();
                    aspNetUser.User = personnelAcf.ExtractUser();
                    aspNetUser.UserName = PersonnelACF.GetUserName(aspNetUser.User, school.Id, aspNetUser.PhoneNumber);
                    aspNetUser.NormalizedUserName = aspNetUser.UserName.ToUpperInvariant();
                    
                    this.aspNetUserRepository.Create(aspNetUser);
                    this.IdsLog.Add(aspNetUser.Id);

                    this.aspNetUserRepository.LinkSchool(aspNetUser, school.Id);
                }
                else
                {
                    if (!Quiet)
                        Logger.LogInformation($"Updating Personnel User with PhoneNumber: {aspNetUser.PhoneNumber}");

                    var userFrom = personnelAcf.ExtractUser();
                    var aspNetUserFrom = personnelAcf.ToContext();
                    aspNetUserFrom.UserName = PersonnelACF.GetUserName(userFrom, school.Id, aspNetUser.PhoneNumber);
                    aspNetUserFrom.NormalizedUserName = aspNetUserFrom.UserName.ToUpperInvariant();

                    this.aspNetUserRepository.Update(aspNetUser, aspNetUserFrom, userFrom);
                    this.IdsLog.Add(aspNetUser.Id);
                }

                if (!Quiet)
                    Logger.LogInformation("Linking with the AspNetUserRoles of Personnel User");

                if (!aspNetUserRepository.HasRole(aspNetUser, personnelAcf.RoleType))
                    aspNetUserRepository.LinkRole(aspNetUser, personnelAcf.RoleType);
                
                // Delete any other roles the user might have
                // The only possible scenario where 2 roles are alowed is: a staff role + parent
                aspNetUserRepository.DeleteRoles(aspNetUser, new Role[2] { personnelAcf.RoleType, Role.Parent });

                if (!Quiet)
                    Logger.LogInformation("Linking with the Courses of Personnel User");

                short[] userCourseCodes = personnelAcf.ExtractCourseCodes();
                
                var userCourses = this.SchoolRepository.FindCourses(school.Id);
                if (userCourseCodes.Any())
                    userCourses = userCourses.Where(c => userCourseCodes.Contains(c.Code));

                List<int> userCourseIds = userCourses.Select(c => c.Id).ToList();

                aspNetUserRepository.LinkCourses(aspNetUser, userCourseIds, deleteAdditionalLinks: true);
            }

            Logger.LogInformation("Personnel synchronization finished");
            Logger.LogInformation("--------------------------------");
        }

    }
}
