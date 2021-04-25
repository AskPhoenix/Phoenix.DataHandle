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

        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.Client);

        public ClientService(PhoenixContext phoenixContext, ILogger<WPService> logger,
            string specificSchoolUnique = null, bool deleteAdditional = false, bool quiet = false)
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional, quiet)
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
                if (!this.TryFindSchool(clientPost, out School school))
                    continue;

                ClientACF clientAcf = (ClientACF)(await WordPressClientWrapper.GetAcfAsync<ClientACF>(clientPost.Id)).WithTitleCase();
                clientAcf.SchoolUnique = new SchoolUnique(clientPost.GetTitle());

                var parents = clientAcf.ExtractParents();
                var parentUsers = clientAcf.ExtractParentUsers();
                int parentsNum = parents.Count;
                List<int> parentIds = new List<int>(parentsNum);

                for (int i = 0; i < parentsNum; i++)
                {
                    parents[i].UserName = ClientACF.GetUserName(parentUsers[i], school.Id, parents[i].PhoneNumber);
                    parents[i].NormalizedUserName = parents[i].UserName.ToUpperInvariant();

                    string parentPhoneString = i == 0 ? clientAcf.Parent1PhoneString : clientAcf.Parent2PhoneString;
                    AspNetUsers parent = this.aspNetUserRepository.Find().
                            SingleOrDefault(u => u.User.IsSelfDetermined && u.PhoneNumber == parentPhoneString);

                    if (parent is null)
                    {
                        if (!Quiet)
                            Logger.LogInformation($"Adding Parent {i + 1} with PhoneNumber {parentPhoneString}.");

                        parent = parents[i];
                        parent.User = parentUsers[i];

                        this.aspNetUserRepository.Create(parent);
                        this.aspNetUserRepository.LinkRole(parent, Role.Parent);

                        if (!Quiet)
                            Logger.LogInformation($"Linking Parent {i + 1} with School");
                        this.aspNetUserRepository.LinkSchool(parent, school.Id);
                    }
                    else
                    {
                        if (!Quiet)
                            Logger.LogInformation($"Updating Parent {i + 1} with PhoneNumber {parentPhoneString}.");

                        this.aspNetUserRepository.Update(parent, parents[i], parentUsers[i]);
                        if (!aspNetUserRepository.HasRole(parent, Role.Parent))
                            this.aspNetUserRepository.LinkRole(parent, Role.Parent);
                    }

                    parentIds.Add(parent.Id);
                }

                AspNetUsers student = null;
                if (clientAcf.IsSelfDetermined)
                    student = await this.aspNetUserRepository.Find(checkUnique: clientAcf.MatchesUnique);
                else
                {
                    if (!clientAcf.HasParent1 && !clientAcf.HasParent2)
                        Logger.LogError($"Non self determined users must have at least one parent. Post {clientPost.Id} is skipped.");

                    student = aspNetUserRepository.FindChild(parentIds.First(), clientAcf.StudentFirstName, clientAcf.StudentLastName);
                }

                if (student is null)
                {
                    if (!Quiet)
                        Logger.LogInformation($"Adding Student with (Parent) PhoneNumber: {clientAcf.TopPhoneNumber}");

                    student = clientAcf.ToContext();
                    student.User = clientAcf.ExtractUser();
                    student.UserName = ClientACF.GetUserName(student.User, school.Id, clientAcf.TopPhoneNumber);
                    student.NormalizedUserName = student.UserName.ToUpperInvariant();

                    this.aspNetUserRepository.Create(student);
                    this.IdsLog.Add(student.Id);

                    this.aspNetUserRepository.LinkSchool(student, school.Id);
                    this.aspNetUserRepository.LinkRole(student, Role.Student);
                }
                else
                {
                    if (!Quiet)
                        Logger.LogInformation($"Updating Student with (Parent) PhoneNumber: {clientAcf.TopPhoneNumber}");

                    var updatedUser = clientAcf.ExtractUser();
                    var aspNetUserFrom = clientAcf.ToContext();
                    aspNetUserFrom.UserName = ClientACF.GetUserName(updatedUser, school.Id, clientAcf.TopPhoneNumber);
                    aspNetUserFrom.NormalizedUserName = aspNetUserFrom.UserName.ToUpperInvariant();

                    this.aspNetUserRepository.Update(student, aspNetUserFrom, updatedUser);
                    this.IdsLog.Add(student.Id);

                    if (!aspNetUserRepository.HasRole(student, Role.Student))
                        this.aspNetUserRepository.LinkRole(student, Role.Student);
                }

                foreach (int parId in parentIds)
                {
                    if (aspNetUserRepository.FindChild(parId, clientAcf.StudentFirstName, clientAcf.StudentLastName) is null)
                    {
                        if (!Quiet)
                            Logger.LogInformation($"Linking with the Parents of Student");

                        this.aspNetUserRepository.LinkParenthood(parId, student.Id);
                    }
                }

                if (!Quiet)
                    Logger.LogInformation("Linking with the Courses of Student");

                short[] userCourseCodes = clientAcf.ExtractCourseCodes();

                var userCourses = this.SchoolRepository.FindCourses(school.Id);
                if (userCourseCodes.Any())
                    userCourses = userCourses.Where(c => userCourseCodes.Contains(c.Code));

                List<int> userCourseIds = userCourses.Select(c => c.Id).ToList();

                aspNetUserRepository.LinkCourses(student, userCourseIds, deleteAdditionalLinks: true);
            }

            Logger.LogInformation("Client synchronization finished");
            Logger.LogInformation("--------------------------------");
        }

    }
}
