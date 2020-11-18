using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Models.Extensions;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.WordPress.Models;
using Phoenix.DataHandle.WordPress.Utilities;
using Phoenix.DataHandle.WordPress.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace Phoenix.DataHandle.Services
{
    public abstract class WPService
    {
        private readonly SchoolRepository schoolRepository;
        private readonly CourseRepository courseRepository;

        protected const int PostsPerPage = 20;
        protected readonly ILogger<WPService> _logger;

        protected abstract int CategoryId { get; }
        protected List<int> IdsLog { get; set; } = new List<int>(); //Helping log for the Delete method
        protected bool SpecificSchoolOnly => !string.IsNullOrEmpty(SpecificSchoolUnique);
        protected string SpecificSchoolUnique { get; }
        protected bool DeleteAdditional { get; }

        protected WPService(PhoenixContext phoenixContext, ILogger<WPService> logger, string specificSchoolUnique, bool deleteAdditional)
        {
            this.schoolRepository = new SchoolRepository(phoenixContext);
            this.courseRepository = new CourseRepository(phoenixContext);

            this._logger = logger;
            this.SpecificSchoolUnique = specificSchoolUnique;
            this.DeleteAdditional = deleteAdditional;
        }

        public abstract Task SynchronizeAsync();
        public abstract void DeleteComplement();

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            if (this.SpecificSchoolOnly)
                return await WordPressClientWrapper.GetPostsByCategoryBySchoolAsync(this.CategoryId, this.SpecificSchoolUnique, PostsPerPage);
            
            return await WordPressClientWrapper.GetPostsByCategoryAsync(this.CategoryId, PostsPerPage);
        }

        public bool TryGetSchoolIdFromPost(Post post, out int schoolId)
        {
            SchoolACF acfSchool = (SchoolACF)new SchoolACF(post.GetSchoolUnique()).WithTitleCase();
            School school = schoolRepository.Find(acfSchool.MatchesUnique).Result;

            bool tore = this.TryGetId(school, out schoolId);

            if (!tore)
                _logger.LogError($"The School \"{acfSchool.Name} - {acfSchool.City}\" was not found");

            return tore;
        }

        public bool TryGetCourseId(int schoolId, short courseCode, out int courseId)
        {
            CourseACF acfCourse = new CourseACF(schoolId, courseCode);
            Course course = courseRepository.Find(acfCourse.MatchesUnique).Result;

            bool tore = this.TryGetId(course, out courseId);
            
            if (!tore)
                _logger.LogError($"The Course with code {courseCode} was not found in School with id {schoolId}");

            return tore;
        }

        private bool TryGetId(IModelEntity entity, out int id)
        {
            if (entity == null)
            {
                id = -1;
                return false;
            }

            id = entity.Id;
            return true;
        }
    }
}
