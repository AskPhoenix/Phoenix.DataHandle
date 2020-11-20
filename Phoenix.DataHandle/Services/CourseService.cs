using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.WordPress;
using Phoenix.DataHandle.WordPress.Models;
using Phoenix.DataHandle.WordPress.Utilities;
using Phoenix.DataHandle.WordPress.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Services
{
    public class CourseService : WPService
    {
        private readonly CourseRepository courseRepository;
        private readonly BookRepository bookRepository;

        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.Course);

        public CourseService(PhoenixContext phoenixContext, ILogger<WPService> logger)
            : this(phoenixContext, logger, false, null)
        { }

        public CourseService(PhoenixContext phoenixContext, ILogger<WPService> logger, bool deleteAdditional, string specificSchoolUnique)
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional)
        {
            this.courseRepository = new CourseRepository(phoenixContext);
            this.bookRepository = new BookRepository(phoenixContext);
        }

        public override void DeleteComplement()
        {
            throw new NotImplementedException();
        }

        public override async Task SynchronizeAsync()
        {
            _logger.LogInformation("Courses and Books synchronization started");

            var coursePosts = await this.GetAllPostsAsync();
            foreach (var coursePost in coursePosts)
            {
                if (!this.TryGetSchoolIdFromPost(coursePost, out int schoolId))
                    continue;

                CourseACF acfCourse = (CourseACF)(await WordPressClientWrapper.GetAcfAsync<CourseACF>(coursePost.Id)).WithTitleCase();
                acfCourse.SchoolId = schoolId;

                var curCourse = await courseRepository.Find(acfCourse.MatchesUnique);
                var ctxCourse = acfCourse.ToContext();
                if (curCourse == null)
                {
                    _logger.LogInformation($"Adding Course: {coursePost.GetTitle()}");
                    
                    courseRepository.Create(ctxCourse);
                    this.IdsLog.Add(ctxCourse.Id);
                }
                else
                {
                    _logger.LogInformation($"Updating Course: {coursePost.GetTitle()}");
                    courseRepository.Update(curCourse, ctxCourse);
                    this.IdsLog.Add(curCourse.Id);
                }

                _logger.LogInformation($"Synchronizing Books of Course: {coursePost.GetTitle()}");

                var books = acfCourse.ExtractBooks();
                List<int> bookIds = new List<int>(books.Count());

                foreach (var book in books)
                {
                    if (!bookRepository.Find().Any(b => b.NormalizedName == book.NormalizedName))
                    {
                        _logger.LogInformation($"Adding Book: {book.Name}");
                        bookRepository.Create(book);
                        bookIds.Add(book.Id);
                    }
                    else
                    {
                        _logger.LogInformation($"Book \"{book.Name}\" already exists");
                        bookIds.Add((await bookRepository.Find(b => b.NormalizedName == book.NormalizedName)).Id);
                    }
                }

                _logger.LogInformation($"Linking Books with Course {coursePost.GetTitle()}");
                
                var bookIdsToExclude = courseRepository.GetLinkedBooks(curCourse ?? ctxCourse).Select(b => b.Id);
                bookIds.RemoveAll(id => bookIdsToExclude.Contains(id));
                courseRepository.LinkBooks(curCourse ?? ctxCourse, bookIds);
            }

            _logger.LogInformation("Courses and Books synchronization finished");
            _logger.LogInformation("--------------------------------");
        }
    }
}
