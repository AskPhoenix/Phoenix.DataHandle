using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.WordPress.Models;
using Phoenix.DataHandle.WordPress.Models.Uniques;
using Phoenix.DataHandle.WordPress.Utilities;
using Phoenix.DataHandle.WordPress.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace Phoenix.DataHandle.Services
{
    public abstract class WPService
    {
        protected SchoolRepository SchoolRepository { get;}
        protected CourseRepository CourseRepository { get; }
        protected ILogger Logger { get; }
        
        protected abstract int CategoryId { get; }
        protected List<int> IdsLog { get; set; } = new List<int>(); //Helping log for the Delete method
        protected bool SpecificSchoolOnly => !string.IsNullOrEmpty(SpecificSchoolUnique);
        protected string SpecificSchoolUnique { get; }
        protected bool DeleteAdditional { get; }
        protected bool Quiet { get; }

        protected WPService(PhoenixContext phoenixContext, ILogger<WPService> logger, 
            string specificSchoolUnique = null, bool deleteAdditional = false, bool quiet = false)
        {
            this.SchoolRepository = new SchoolRepository(phoenixContext);
            this.SchoolRepository.Include(s => s.SchoolSettings);
            this.CourseRepository = new CourseRepository(phoenixContext);
            this.CourseRepository.Include(c => c.School);

            this.Logger = logger;
            this.SpecificSchoolUnique = specificSchoolUnique;
            this.DeleteAdditional = deleteAdditional;
            this.Quiet = quiet;
        }

        public abstract Task SynchronizeAsync();
        public abstract void DeleteComplement();

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            if (this.SpecificSchoolOnly)
                return await WordPressClientWrapper.GetPostsForSchoolAsync(this.CategoryId, this.SpecificSchoolUnique);
            
            return await WordPressClientWrapper.GetPostsAsync(this.CategoryId);
        }

        public bool TryFindSchool(Post post, out School school)
        {
            SchoolUnique schoolUnique = new SchoolUnique(post.GetTitle());
            SchoolACF acfSchool = new SchoolACF(schoolUnique);
            school = this.SchoolRepository.Find(acfSchool.MatchesUnique).Result;

            bool isValid = school != null;
            if (!isValid)
                this.Logger.LogError($"The School \"{schoolUnique.NormalizedSchoolName} - {schoolUnique.NormalizedSchoolCity}\" was not found.");

            return isValid;
        }

        public bool TryFindCourse(Post post, int schoolId, out Course course)
        {
            CourseUnique courseUnique = new CourseUnique(post.GetTitle());
            CourseACF acfCourse = new CourseACF(courseUnique);
            course = this.CourseRepository.Find(acfCourse.MatchesUnique).Result;

            bool isValid = course != null;
            if (!isValid)
                this.Logger.LogError($"The Course {courseUnique} was not found in School with id {schoolId}.");

            return isValid;
        }
    }
}
