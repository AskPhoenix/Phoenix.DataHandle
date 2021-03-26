using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.WordPress;
using Phoenix.DataHandle.WordPress.Models;
using Phoenix.DataHandle.WordPress.Models.Uniques;
using Phoenix.DataHandle.WordPress.Utilities;
using Phoenix.DataHandle.WordPress.Wrappers;
using System;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Services
{
    public class ScheduleService : WPService
    {
        private readonly ScheduleRepository scheduleRepository;
        private readonly ClassroomRepository classroomRepository;

        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.Schedule);

        public ScheduleService(PhoenixContext phoenixContext, ILogger<WPService> logger,
            string specificSchoolUnique = null, bool deleteAdditional = false)
            : base(phoenixContext, logger, specificSchoolUnique, deleteAdditional)
        {
            this.scheduleRepository = new ScheduleRepository(phoenixContext);
            this.classroomRepository = new ClassroomRepository(phoenixContext);
        }

        public override void DeleteComplement()
        {
            throw new NotImplementedException();
        }

        public override async Task SynchronizeAsync()
        {
            Logger.LogInformation("Schedules and Classrooms synchronization started");

            var schedulePosts = await this.GetAllPostsAsync();
            foreach (var schedulePost in schedulePosts)
            {
                if (!this.TryFindSchoolId(schedulePost, out int schoolId) || !this.TryFindCourseId(schedulePost, out int courseId))
                    continue;

                ScheduleACF acfSchedule = (ScheduleACF)(await WordPressClientWrapper.GetAcfAsync<ScheduleACF>(schedulePost.Id)).WithTitleCase();
                acfSchedule.SchoolUnique = new SchoolUnique(schedulePost.GetTitle());
                Classroom classroom = null;

                Logger.LogInformation($"Synchronizing Classroom of Schedule: {schedulePost.GetTitle()}");
                if (!string.IsNullOrEmpty(acfSchedule.ClassroomName))
                {
                    classroom = await this.classroomRepository.Find(c => c.SchoolId == schoolId && c.NormalizedName == acfSchedule.ClassroomName.ToUpperInvariant());
                    if (classroom is null)
                    {
                        Logger.LogInformation($"Adding Classroom {acfSchedule.ClassroomName} in School with id {schoolId}");
                        classroom = new Classroom() 
                        {
                            SchoolId = schoolId,
                            Name = acfSchedule.ClassroomName,
                            NormalizedName = acfSchedule.ClassroomName.ToUpperInvariant()
                        };

                        this.classroomRepository.Create(classroom);
                    }
                }

                var curSchedule = await this.scheduleRepository.Find(acfSchedule.MatchesUnique);
                if (curSchedule is null)
                {
                    Logger.LogInformation($"Adding Schedule: {schedulePost.GetTitle()}");

                    var ctxSchedule = acfSchedule.ToContext();
                    ctxSchedule.CourseId = courseId;
                    if (classroom != null)
                        ctxSchedule.ClassroomId = classroom.Id;

                    scheduleRepository.Create(ctxSchedule);
                    this.IdsLog.Add(ctxSchedule.Id);
                }
                else
                {
                    Logger.LogInformation($"Updating Schedule: {schedulePost.GetTitle()}");

                    scheduleRepository.Update(curSchedule, acfSchedule.ToContext());
                    this.IdsLog.Add(curSchedule.Id);
                }
            }

            Logger.LogInformation("Schedules and Classrooms synchronization finished");
            Logger.LogInformation("--------------------------------");
        }
    }
}
