using Microsoft.Extensions.Logging;
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
    public class CourseService : WPService
    {
        private readonly BookRepository bookRepository;

        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.Course);

        public CourseService(PhoenixContext phoenixContext, ILogger<WPService> logger,
            string specificSchoolUnique = null, bool deleteAdditional = false)
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional) 
        {
            this.bookRepository = new BookRepository(phoenixContext);
        }

        public override void DeleteComplement()
        {
            throw new NotImplementedException();
        }

        public override async Task SynchronizeAsync()
        {
            Logger.LogInformation("Courses and Books synchronization started");

            var coursePosts = await this.GetAllPostsAsync();
            foreach (var coursePost in coursePosts)
            {
                if (!this.TryFindSchoolId(coursePost, out int schoolId))
                    continue;

                CourseACF acfCourse = (CourseACF)(await WordPressClientWrapper.GetAcfAsync<CourseACF>(coursePost.Id)).WithTitleCase();
                acfCourse.SchoolUnique = new SchoolUnique(coursePost.GetTitle());

                var curCourse = await this.CourseRepository.Find(acfCourse.MatchesUnique);
                var ctxCourse = acfCourse.ToContext();
                if (curCourse is null)
                {
                    Logger.LogInformation($"Adding Course: {coursePost.GetTitle()}");
                    
                    this.CourseRepository.Create(ctxCourse);
                    this.IdsLog.Add(ctxCourse.Id);
                }
                else
                {
                    Logger.LogInformation($"Updating Course: {coursePost.GetTitle()}");
                    this.CourseRepository.Update(curCourse, ctxCourse);
                    this.IdsLog.Add(curCourse.Id);
                }

                Logger.LogInformation($"Synchronizing Books of Course: {coursePost.GetTitle()}");

                var books = acfCourse.ExtractBooks();
                List<int> bookIds = new List<int>(books.Count());

                foreach (var book in books)
                {
                    if (!bookRepository.Find().Any(b => b.NormalizedName == book.NormalizedName))
                    {
                        Logger.LogInformation($"Adding Book: {book.Name}");
                        bookRepository.Create(book);
                        bookIds.Add(book.Id);
                    }
                    else
                    {
                        Logger.LogInformation($"Book \"{book.Name}\" already exists");
                        bookIds.Add((await bookRepository.Find(b => b.NormalizedName == book.NormalizedName)).Id);
                    }
                }

                Logger.LogInformation($"Linking Books with Course {coursePost.GetTitle()}");
                
                var bookIdsToExclude = this.CourseRepository.GetLinkedBooks(curCourse ?? ctxCourse).Select(b => b.Id);
                bookIds.RemoveAll(id => bookIdsToExclude.Contains(id));
                this.CourseRepository.LinkBooks(curCourse ?? ctxCourse, bookIds);
            }

            Logger.LogInformation("Courses and Books synchronization finished");
            Logger.LogInformation("--------------------------------");
        }
    }
}
