using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.WordPress;
using Phoenix.DataHandle.WordPress.Models;
using Phoenix.DataHandle.WordPress.Models.Uniques;
using Phoenix.DataHandle.WordPress.Utilities;
using Phoenix.DataHandle.WordPress.Wrappers;
using System;
using System.Linq;
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
                if (!this.TryFindSchool(schedulePost, out School school) || !this.TryFindCourse(schedulePost, school.Id, out Course course))
                    continue;

                ScheduleACF scheduleAcf = (ScheduleACF)(await WordPressClientWrapper.GetAcfAsync<ScheduleACF>(schedulePost.Id)).WithTitleCase();
                scheduleAcf.SchoolUnique = new SchoolUnique(schedulePost.GetTitle());
                scheduleAcf.SchoolTimeZone = school.SchoolSettings.TimeZone;
                Classroom classroom = null;

                if (!string.IsNullOrEmpty(scheduleAcf.ClassroomName))
                {
                    classroom = await this.classroomRepository.
                        Find(c => c.SchoolId == school.Id && c.NormalizedName == scheduleAcf.ClassroomName.ToUpperInvariant());
                    if (classroom is null)
                    {
                        Logger.LogInformation($"Adding Classroom {scheduleAcf.ClassroomName} in School with id {school.Id}");
                        classroom = new Classroom() 
                        {
                            SchoolId = school.Id,
                            Name = scheduleAcf.ClassroomName,
                            NormalizedName = scheduleAcf.ClassroomName.ToUpperInvariant()
                        };

                        this.classroomRepository.Create(classroom);
                    }
                    else
                    {
                        Logger.LogInformation($"Updating Classroom {scheduleAcf.ClassroomName} {classroom.Id} in School with id {school.Id}");
                        this.classroomRepository.Update(classroom);
                    }
                }

                var schedule = await this.scheduleRepository.Find(scheduleAcf.MatchesUnique);
                if (schedule is null)
                {
                    Logger.LogInformation($"Adding Schedule: {schedulePost.GetTitle()}");

                    schedule = scheduleAcf.ToContext();
                    schedule.CourseId = course.Id;
                    schedule.ClassroomId = classroom?.Id;

                    scheduleRepository.Create(schedule);
                    this.IdsLog.Add(schedule.Id);
                }
                else
                {
                    Logger.LogInformation($"Updating Schedule: {schedulePost.GetTitle()}");

                    var scheduleFrom = scheduleAcf.ToContext();
                    scheduleFrom.ClassroomId = classroom?.Id;

                    scheduleRepository.Update(schedule, scheduleFrom);
                    this.IdsLog.Add(schedule.Id);
                }
            }

            Logger.LogInformation("Schedules and Classrooms synchronization finished");
            Logger.LogInformation("--------------------------------");
        }
    }
}
