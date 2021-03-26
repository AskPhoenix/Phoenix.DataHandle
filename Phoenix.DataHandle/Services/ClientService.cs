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
    public class ClientService : WPService
    {
        private readonly AspNetUserRepository aspNetUserRepository;

        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.Personnel);

        public ClientService(PhoenixContext phoenixContext, ILogger<WPService> logger,
            string specificSchoolUnique = null, bool deleteAdditional = false)
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional)
        {
            this.aspNetUserRepository = new AspNetUserRepository(phoenixContext);
            this.aspNetUserRepository.Include(u => u.User);
            this.aspNetUserRepository.Include(u => u.ParenthoodChild);
            this.aspNetUserRepository.Include(u => u.ParenthoodParent);
        }

        public override void DeleteComplement()
        {
            throw new NotImplementedException();
        }

        public override async Task SynchronizeAsync()
        {
            Logger.LogInformation("Client synchronization started");

            var clientPosts = (await this.GetAllPostsAsync());
            foreach (var clientPost in clientPosts)
            {
                if (!this.TryFindSchoolId(clientPost, out int schoolId))
                    continue;

                ClientACF clientACF = (ClientACF)(await WordPressClientWrapper.GetAcfAsync<ClientACF>(clientPost.Id)).WithTitleCase();
                clientACF.SchoolUnique = new SchoolUnique(clientPost.GetTitle());

                var student = await this.aspNetUserRepository.Find(checkUnique: clientACF.MatchesUnique);
                if (student is null)
                {
                    Logger.LogInformation($"Adding Student with (Affiliated) PhoneNumber: {student.PhoneNumber ?? student.AffiliatedPhoneNumber}");

                    //TODO: Check if User needs to be created separately
                    //aspNetUser.User = personnelACF.ExtractUser();
                    student = clientACF.ToContext();
                    aspNetUserRepository.Create(student, clientACF.ExtractUser());

                    this.aspNetUserRepository.Create(student);
                    this.IdsLog.Add(student.Id);

                    this.aspNetUserRepository.LinkSchool(student, schoolId);
                    this.aspNetUserRepository.LinkRole(student, Role.Student);
                }
                else
                {
                    Logger.LogInformation($"Updating Personnel User with PhoneNumber: {student.PhoneNumber}");
                    this.aspNetUserRepository.Update(student, clientACF.ToContext(), clientACF.ExtractUser());
                    this.IdsLog.Add(student.Id);
                }

                Logger.LogInformation("Linking with the Parents of Student");

                var parents = clientACF.ExtractParents();
                var parentUsers = clientACF.ExtractParentUsers();
                int parentsNum = parents.Count;

                AspNetUsers parent = null;
                for (int i = 0; i < parentsNum; i++)
                {
                    if (i == 0)
                        parent = this.aspNetUserRepository.Find().
                            SingleOrDefault(u => u.User.IsSelfDetermined && u.PhoneNumber == clientACF.Parent1PhoneNumber.ToString());
                    else
                        parent = this.aspNetUserRepository.Find().
                            SingleOrDefault(u => u.User.IsSelfDetermined && u.PhoneNumber == clientACF.Parent2PhoneNumber.ToString());

                    if (parent == null)
                    {
                        parent = parents[i];
                        this.aspNetUserRepository.Create(parent, parentUsers[i]);
                        this.aspNetUserRepository.LinkParenthood(parent, student);
                        this.aspNetUserRepository.LinkRole(parent, Role.Parent);
                    }
                    else
                        this.aspNetUserRepository.Update(parent, parents[i], parentUsers[i]);

                    parent = null;
                }

                Logger.LogInformation("Linking with the Courses of Student");

                List<int> userCourseIds;
                if (string.IsNullOrEmpty(clientACF.CourseCodesString))
                {
                    userCourseIds = this.SchoolRepository.FindCourses(schoolId).Select(c => c.Id).ToList();
                }
                else
                {
                    short[] courseCodes = clientACF.ExtractCourseCodes();
                    userCourseIds = new List<int>(courseCodes.Length);
                    foreach (short courseCode in courseCodes)
                    {
                        if (!this.TryFindCourseId(clientPost, out int courseId))
                            continue;
                        userCourseIds.Add(courseId);
                    }
                }

                aspNetUserRepository.LinkCourses(student, userCourseIds);
            }

            Logger.LogInformation("Client synchronization finished");
            Logger.LogInformation("--------------------------------");
        }

    }
}
