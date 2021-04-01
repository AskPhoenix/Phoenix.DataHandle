using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.WordPress;
using Phoenix.DataHandle.WordPress.Models;
using Phoenix.DataHandle.WordPress.Utilities;
using Phoenix.DataHandle.WordPress.Wrappers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Services
{
    public class SchoolService : WPService
    {
        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.SchoolInformation);

        public SchoolService(PhoenixContext phoenixContext, ILogger<WPService> logger, 
            string specificSchoolUnique = null, bool deleteAdditional = false, bool quiet = false)
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional, quiet) { }

        public override void DeleteComplement()
        {
            //TODO: Find the correct order to delete the additional entries
            //Deleting a School will affect all the tables attached to it, so everything will need to be deleted.

            throw new NotImplementedException();
        }

        public override async Task SynchronizeAsync()
        {
            var schoolPosts = await this.GetAllPostsAsync();
            Logger.LogInformation($"Schools synchronization started ({schoolPosts.Count()} Schools found)");

            foreach (var schoolPost in schoolPosts)
            {
                SchoolACF schoolAcf = (SchoolACF)(await WordPressClientWrapper.GetAcfAsync<SchoolACF>(schoolPost.Id)).WithTitleCase();
                var school = await this.SchoolRepository.Find(checkUnique: schoolAcf.MatchesUnique);

                if (school is null)
                {
                    if (!Quiet)
                        Logger.LogInformation($"Adding School: {schoolPost.GetTitle()}");

                    school = schoolAcf.ToContext();
                    school.SchoolSettings = schoolAcf.ExtractSchoolSettings();

                    this.SchoolRepository.Create(school);
                    this.IdsLog.Add(school.Id);
                }
                else
                {
                    if (!Quiet)
                        Logger.LogInformation($"Updating School: {schoolPost.GetTitle()}");
                    this.SchoolRepository.Update(school, schoolAcf.ToContext(), schoolAcf.ExtractSchoolSettings());
                    this.IdsLog.Add(school.Id);
                }
            }

            Logger.LogInformation("Schools synchronization finished");
            Logger.LogInformation("--------------------------------");
        }
    }
}
