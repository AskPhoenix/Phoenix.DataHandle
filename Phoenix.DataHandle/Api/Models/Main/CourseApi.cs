using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class CourseApi : ICourse, IModelApi
    {
        private CourseApi()
        {
            this.Grades = new List<GradeApi>();
            this.Lectures = new List<LectureApi>();
            this.Schedules = new List<ScheduleApi>();
            this.Teachers = new List<AspNetUserApi>();

            this.Books = new List<IBook>();
            this.Broadcasts = new List<IBroadcast>();
        }

        // TODO: Check if DateTimeOffset works with JSON
        [JsonConstructor]
        public CourseApi(int id, short code, SchoolApi school, string name, string? subcourse, string level, string group, string? comments,
            DateTimeOffset firstDate, DateTimeOffset lastDate, List<GradeApi>? grades, List<LectureApi>? lectures,
            List<ScheduleApi>? schedules, List<AspNetUserApi>? teachers)
            : this()
        {
            if (school is null)
                throw new ArgumentNullException(nameof(school));
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            if (level is null)
                throw new ArgumentNullException(nameof(level));
            if (group is null)
                throw new ArgumentNullException(nameof(group));

            this.Id = id;
            this.Code = code;
            this.School = school;
            this.Name = name;
            this.SubCourse = subcourse;
            this.Level = level;
            this.Group = group;
            this.Comments = comments;
            this.FirstDate = firstDate;
            this.LastDate = lastDate;

            if (grades is not null)
                this.Grades = grades;
            if (lectures is not null)
                this.Lectures = lectures;
            if (schedules is not null)
                this.Schedules = schedules;
            if (teachers is not null)
                this.Teachers = teachers;
        }

        public CourseApi(ICourse course, int id = 0)
            : this(id, course.Code, new SchoolApi(course.School), course.Name, course.SubCourse, course.Level, course.Group, 
                  course.Comments, course.FirstDate, course.LastDate, null, null, null, null)
        {
            this.Grades = course.Grades.Select(g => new GradeApi(g)).ToList();
            this.Lectures = course.Lectures.Select(l => new LectureApi(l)).ToList();
            this.Schedules = course.Schedules.Select(s => new ScheduleApi(s)).ToList();
            this.Teachers = course.Teachers.Select(t => new AspNetUserApi(t)).ToList();
        }

        public CourseApi(Course course)
            : this(course, course.Id)
        {
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "code")]
        public short Code { get; }

        [JsonProperty(PropertyName = "school")]
        public SchoolApi School { get; } = null!;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "subcourse")]
        public string? SubCourse { get; }

        [JsonProperty(PropertyName = "level")]
        public string Level { get; } = null!;

        [JsonProperty(PropertyName = "group")]
        public string Group { get; } = null!;

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        [JsonProperty(PropertyName = "first_date")]
        public DateTimeOffset FirstDate { get; }

        [JsonProperty(PropertyName = "last_date")]
        public DateTimeOffset LastDate { get; }

        [JsonProperty(PropertyName = "grades")]
        public List<GradeApi> Grades { get; }

        [JsonProperty(PropertyName = "lectures")]
        public List<LectureApi> Lectures { get; }

        [JsonProperty(PropertyName = "schedules")]
        public List<ScheduleApi> Schedules { get; }

        [JsonProperty(PropertyName = "teachers")]
        public List<AspNetUserApi> Teachers { get; }


        ISchool ICourse.School => this.School;
        
        IEnumerable<IGrade> ICourse.Grades => this.Grades;
        
        IEnumerable<ILecture> ICourse.Lectures => this.Lectures;
        
        IEnumerable<ISchedule> ICourse.Schedules => this.Schedules;
        
        [JsonIgnore]
        public IEnumerable<IBook> Books { get; }
        
        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }
        
        IEnumerable<IAspNetUser> ICourse.Teachers => this.Teachers;
    }
}
