using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.WordPress;
using Phoenix.DataHandle.WordPress.Models;
using Phoenix.DataHandle.WordPress.Utilities;
using Phoenix.DataHandle.WordPress.Wrappers;
using System;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Services
{
    public class SchoolService : WPService
    {
        private readonly SchoolRepository schoolRepository;
        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.School);

        public SchoolService(PhoenixContext phoenixContext, ILogger<WPService> logger)
            : this(phoenixContext, logger, false, null) 
        { }

        public SchoolService(PhoenixContext phoenixContext, ILogger<WPService> logger, bool deleteAdditional, string specificSchoolUnique)
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional)
        {
            schoolRepository = new SchoolRepository(phoenixContext);
        }

        public override void DeleteComplement()
        {
            //TODO: Find the correct order to delete the additional entries
            //Deleting a School will affect all the tables attached to it, so everything will need to be deleted.

            throw new NotImplementedException();
        }

        public override async Task SynchronizeAsync()
        {
            _logger.LogInformation("Schools synchronization started");

            var schoolPosts = await this.GetAllPostsAsync();
            foreach (var schoolPost in schoolPosts)
            {
                SchoolACF acfSchool = (SchoolACF)(await WordPressClientWrapper.GetAcfAsync<SchoolACF>(schoolPost.Id)).WithTitleCase();
                var curSchool = await schoolRepository.Find(checkUnique: acfSchool.MatchesUnique);

                if (curSchool == null)
                {
                    _logger.LogInformation($"Adding School: {schoolPost.GetTitle()}");

                    var ctxSchool = acfSchool.ToContext();
                    schoolRepository.Create(ctxSchool);
                    this.IdsLog.Add(ctxSchool.Id);
                }
                else
                {
                    _logger.LogInformation($"Updating School: {schoolPost.GetTitle()}");
                    schoolRepository.Update(curSchool, acfSchool.ToContext());
                    this.IdsLog.Add(curSchool.Id);
                }
            }

            _logger.LogInformation("Schools synchronization finished");
            _logger.LogInformation("--------------------------------");
        }
    }
}
