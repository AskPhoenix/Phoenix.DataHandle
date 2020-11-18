using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.WordPress;
using Phoenix.DataHandle.WordPress.Models;
using Phoenix.DataHandle.WordPress.Utilities;
using Phoenix.DataHandle.WordPress.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Services
{
    public class SchoolUserService : WPService
    {
        private readonly AspNetUserRepository aspNetUserRepository;
        private readonly UserRepository userRepository;

        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.SchoolUser);

        public SchoolUserService(PhoenixContext phoenixContext, ILogger<WPService> logger, bool deleteAdditional)
           : this(phoenixContext, logger, deleteAdditional, null)
        { }

        public SchoolUserService(PhoenixContext phoenixContext, ILogger<WPService> logger, bool deleteAdditional, string specificSchoolUnique)
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional)
        {
            this.aspNetUserRepository = new AspNetUserRepository(phoenixContext);
            this.userRepository = new UserRepository(phoenixContext);
        }

        public override void DeleteComplement()
        {
            throw new NotImplementedException();
        }

        public override async Task SynchronizeAsync()
        {
            _logger.LogInformation("School Users synchronization started");

            var schoolUserPosts = await this.GetAllPostsAsync();
            foreach (var schoolUserPost in schoolUserPosts)
            {
                if (!this.TryGetSchoolIdFromPost(schoolUserPost, out int schoolId))
                    continue;

                SchoolUserACF acfSchoolUser = (SchoolUserACF)(await WordPressClientWrapper.GetAcfAsync<SchoolUserACF>(schoolUserPost.Id)).WithTitleCase();
                acfSchoolUser.SchoolId = schoolId;

                AspNetUsers aspNetUser;
                var curUserSchool = await userRepository.FindUserSchoolAsync(acfSchoolUser.MatchesUnique);
                if (curUserSchool == null)
                {
                    _logger.LogInformation($"Adding AspNetUser for School User: {schoolUserPost.GetTitle()}");
                    aspNetUser = acfSchoolUser.ExtractAspNetUser();
                    aspNetUserRepository.Create(aspNetUser);
                    acfSchoolUser.UserId = aspNetUser.Id;

                    _logger.LogInformation($"Adding User for School User: {schoolUserPost.GetTitle()}");
                    userRepository.Create(acfSchoolUser.ExtractUser());

                    _logger.LogInformation($"Adding UserSchool for School User: {schoolUserPost.GetTitle()}");
                    userRepository.LinkSchool(acfSchoolUser.ToContext());
                    
                    //TODO: What log to keep here? Probably multiple ones will be needed
                    //this.IdsLog.Add( ? );
                }
                else
                {
                    _logger.LogInformation($"Updating AspNetUser for School User: {schoolUserPost.GetTitle()}");
                    aspNetUser = await aspNetUserRepository.Find(curUserSchool.AspNetUserId);
                    aspNetUserRepository.Update(aspNetUser, acfSchoolUser.ExtractAspNetUser());

                    _logger.LogInformation($"Updating User for School User: {schoolUserPost.GetTitle()}");
                    var user = await userRepository.Find(curUserSchool.AspNetUserId);
                    userRepository.Update(user, acfSchoolUser.ExtractUser());
                }

                //TODO: Delete old Roles if any
                _logger.LogInformation($"Linking with the AspNetUserRoles of School User: {schoolUserPost.GetTitle()}");
                
                List<Role> rolesToAdd = new List<Role>(2);
                if (!aspNetUserRepository.HasRole(aspNetUser, acfSchoolUser.RoleType))
                    rolesToAdd.Add(acfSchoolUser.RoleType);
                if (!aspNetUserRepository.HasRole(aspNetUser, acfSchoolUser.SecondRoleType))
                    rolesToAdd.Add(acfSchoolUser.SecondRoleType);
                
                aspNetUserRepository.LinkRoles(aspNetUser, rolesToAdd);

                _logger.LogInformation($"Linking with the Courses of School User: {schoolUserPost.GetTitle()}");

                short[] courseCodes = acfSchoolUser.ExtractCourseCodes();
                List<int> userCourseIds = new List<int>(courseCodes.Length);
                foreach (short courseCode in courseCodes)
                {
                    if (!this.TryGetCourseId(schoolId, courseCode, out int courseId))
                        continue;
                    userCourseIds.Add(courseId);
                }

                aspNetUserRepository.LinkCourses(aspNetUser, userCourseIds);
            }

            _logger.LogInformation("School Users synchronization finished");
            _logger.LogInformation("--------------------------------");
        }
    }
}
