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
    public class ScheduleService : WPService
    {
        private readonly ScheduleRepository scheduleRepository;
        private readonly ClassroomRepository classroomRepository;

        protected override int CategoryId => PostCategoryWrapper.GetCategoryId(PostCategory.Schedule);

        public ScheduleService(PhoenixContext phoenixContext, ILogger<WPService> logger)
           : this(phoenixContext, logger, false, null)
        { }

        public ScheduleService(PhoenixContext phoenixContext, ILogger<WPService> logger, bool deleteAdditional, string specificSchoolUnique)
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
            _logger.LogInformation("Schedules and Classrooms synchronization started");

            var schedulePosts = await this.GetAllPostsAsync();
            foreach (var schedulePost in schedulePosts)
            {
                if (!this.TryGetSchoolIdFromPost(schedulePost, out int schoolId))
                    continue;

                ScheduleACF acfSchedule = (ScheduleACF)(await WordPressClientWrapper.GetAcfAsync<ScheduleACF>(schedulePost.Id)).WithTitleCase();
                if (!this.TryGetCourseId(schoolId, acfSchedule.CourseCode, out int courseId))
                    continue;
                acfSchedule.CourseId = courseId;

                _logger.LogInformation($"Synchronizing Classroom of Schedule: {schedulePost.GetTitle()}");
                if (!string.IsNullOrEmpty(acfSchedule.ClassroomName))
                {
                    Classroom classroom = await classroomRepository.Find(c => c.SchoolId == schoolId && c.NormalizedName == acfSchedule.ClassroomName.ToUpperInvariant());
                    if (classroom == null)
                    {
                        _logger.LogInformation($"Adding Classroom {acfSchedule.ClassroomName} in School with id {schoolId}");
                        classroom = new Classroom() { SchoolId = schoolId, Name = acfSchedule.ClassroomName, NormalizedName = acfSchedule.ClassroomName.ToUpperInvariant() };

                        classroomRepository.Create(classroom);
                    }

                    acfSchedule.ClassroomId = classroom.Id;
                }

                var curSchedule = await scheduleRepository.Find(acfSchedule.MatchesUnique);
                if (curSchedule == null)
                {
                    _logger.LogInformation($"Adding Schedule: {schedulePost.GetTitle()}");

                    var ctxSchedule = acfSchedule.ToContext();
                    scheduleRepository.Create(ctxSchedule);
                    this.IdsLog.Add(ctxSchedule.Id);
                }
                else
                {
                    _logger.LogInformation($"Updating Schedule: {schedulePost.GetTitle()}");

                    scheduleRepository.Update(curSchedule, acfSchedule.ToContext());
                    this.IdsLog.Add(curSchedule.Id);
                }
            }

            _logger.LogInformation("Schedules and Classrooms synchronization finished");
            _logger.LogInformation("--------------------------------");
        }
    }
}
