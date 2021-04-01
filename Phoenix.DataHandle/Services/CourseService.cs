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
            string specificSchoolUnique = null, bool deleteAdditional = false, bool quiet = false)
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional, quiet) 
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
                if (!this.TryFindSchool(coursePost, out School school))
                    continue;

                CourseACF courseAcf = (CourseACF)(await WordPressClientWrapper.GetAcfAsync<CourseACF>(coursePost.Id)).WithTitleCase();
                courseAcf.SchoolUnique = new SchoolUnique(coursePost.GetTitle());
                courseAcf.SchoolTimeZone = school.SchoolSettings.TimeZone;

                var course = await this.CourseRepository.Find(courseAcf.MatchesUnique);
                if (course is null)
                {
                    if (!Quiet)
                        Logger.LogInformation($"Adding Course: {coursePost.GetTitle()}");

                    course = courseAcf.ToContext();
                    course.SchoolId = school.Id;

                    this.CourseRepository.Create(course);
                    this.IdsLog.Add(course.Id);
                }
                else
                {
                    if (!Quiet)
                        Logger.LogInformation($"Updating Course: {coursePost.GetTitle()}");
                    this.CourseRepository.Update(course, courseAcf.ToContext());
                    this.IdsLog.Add(course.Id);
                }

                if (!Quiet)
                    Logger.LogInformation($"Synchronizing Books of Course: {coursePost.GetTitle()}");

                var books = courseAcf.ExtractBooks();
                List<int> bookIds = new List<int>(books.Count());

                foreach (var book in books)
                {
                    Book ctxBook = await bookRepository.Find(b => b.NormalizedName == book.NormalizedName);
                    if (ctxBook is null)
                    {
                        if (!Quiet)
                            Logger.LogInformation($"Adding Book: {book.Name}");
                        bookRepository.Create(book);
                        ctxBook = book;
                    }
                    else
                    {
                        if (!Quiet)
                            Logger.LogInformation($"Updating Book: {book.Name}");
                        bookRepository.Update(ctxBook);
                    }
                    
                    bookIds.Add(ctxBook.Id);
                }

                if (!Quiet)
                    Logger.LogInformation($"Linking Books with Course {coursePost.GetTitle()}");
                this.CourseRepository.LinkBooks(course, bookIds, deleteAdditionalLinks: true);
            }

            Logger.LogInformation("Courses and Books synchronization finished");
            Logger.LogInformation("--------------------------------");
        }
    }
}
