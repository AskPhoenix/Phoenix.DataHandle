using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using Phoenix.DataHandle.Repositories;

namespace Phoenix.DataHandle.DataEntry
{
    public static class EntryHelper
    {
        public static async Task<Tuple<List<Lecture>, List<Lecture>>> GenerateLecturesAsync(
            Schedule schedule, LectureRepository lectureRepository)
        {
            if (schedule is null)
                throw new ArgumentNullException(nameof(schedule));

            var course = schedule.Course;
            var school = course.School;
            if (course is null || school is null || school.SchoolSetting is null)
                throw new InvalidOperationException(
                    "Course, School, and SchoolSetting properties cannot not be null.");

            var zone = TimeZoneInfo.FindSystemTimeZoneById(school.SchoolSetting.TimeZone);

            var days = Enumerable
                .Range(0, 1 + course.LastDate.Date.Subtract(course.FirstDate.Date).Days)
                .Select(i => course.FirstDate.Date.AddDays(i))
                .Where(d => d.DayOfWeek == schedule.DayOfWeek);

            var lecturesToCreate = new List<Lecture>(days.Count());
            var lecturesToUpdate = new List<Lecture>(days.Count());

            foreach (var day in days)
            {
                var s = day.Add(schedule.StartTime.TimeOfDay);
                var e = day.Add(schedule.EndTime.TimeOfDay);

                var start = new DateTimeOffset(s, zone.GetUtcOffset(s));
                var end = new DateTimeOffset(e, zone.GetUtcOffset(e));

                var lecture = await lectureRepository.FindUniqueAsync(course.Id, start);

                if (lecture is null)
                {
                    lecture = new()
                    {
                        CourseId = course.Id,
                        ClassroomId = schedule.ClassroomId,
                        ScheduleId = schedule.Id,
                        StartDateTime = start,
                        EndDateTime = end,
                        Occasion = LectureOccasion.Scheduled
                    };

                    lecturesToCreate.Add(lecture);
                }
                else
                {
                    if (lecture.Occasion == LectureOccasion.Scheduled)
                    {
                        lecture.ClassroomId = schedule.ClassroomId;
                        lecture.ScheduleId = schedule.Id;
                        lecture.EndDateTime = end;

                        lecturesToUpdate.Add(lecture);
                    }
                }
            }

            return new(lecturesToCreate, lecturesToUpdate);
        }
    }
}
