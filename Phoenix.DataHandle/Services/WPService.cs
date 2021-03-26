using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Models.Extensions;
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
        protected ILogger<WPService> Logger { get; }
        
        protected abstract int CategoryId { get; }
        protected List<int> IdsLog { get; set; } = new List<int>(); //Helping log for the Delete method
        protected bool SpecificSchoolOnly => !string.IsNullOrEmpty(SpecificSchoolUnique);
        protected string SpecificSchoolUnique { get; }
        protected bool DeleteAdditional { get; }

        protected WPService(PhoenixContext phoenixContext, ILogger<WPService> logger, 
            string specificSchoolUnique = null, bool deleteAdditional = false)
        {
            this.SchoolRepository = new SchoolRepository(phoenixContext);
            this.CourseRepository = new CourseRepository(phoenixContext);
            this.CourseRepository.Include(c => c.School);

            this.Logger = logger;
            this.SpecificSchoolUnique = specificSchoolUnique;
            this.DeleteAdditional = deleteAdditional;
        }

        public abstract Task SynchronizeAsync();
        public abstract void DeleteComplement();

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            if (this.SpecificSchoolOnly)
                return await WordPressClientWrapper.GetPostsForSchoolAsync(this.CategoryId, this.SpecificSchoolUnique);
            
            return await WordPressClientWrapper.GetPostsAsync(this.CategoryId);
        }

        public bool TryFindSchoolId(Post post, out int schoolId)
        {
            SchoolUnique schoolUnique = new SchoolUnique(post.GetTitle());
            SchoolACF acfSchool = (SchoolACF)new SchoolACF(schoolUnique).WithTitleCase();
            School school = this.SchoolRepository.Find(acfSchool.MatchesUnique).Result;

            bool tore = TryGetId(school, out schoolId);

            if (!tore)
                this.Logger.LogError($"The School \"{acfSchool.Name} - {acfSchool.City}\" was not found.");

            return tore;
        }

        public bool TryFindCourseId(Post post, out int courseId)
        {
            courseId = -1;

            bool tore = TryFindSchoolId(post, out int schoolId);
            if (!tore)
                return false;

            CourseUnique courseUnique = new CourseUnique(post.GetTitle());
            CourseACF acfCourse = new CourseACF(courseUnique);
            Course course = this.CourseRepository.Find(acfCourse.MatchesUnique).Result;

            tore = TryGetId(course, out courseId);
            
            if (!tore)
                this.Logger.LogError($"The Course {courseUnique} was not found in School with id {schoolId}.");

            return tore;
        }

        private static bool TryGetId(IModelEntity entity, out int id)
        {
            bool nullEntity = entity is null;
            id = nullEntity ? -1 : entity.Id;

            return nullEntity;
        }
    }
}
