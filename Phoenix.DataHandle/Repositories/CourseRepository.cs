﻿using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override Course Create(Course tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.Create(tModel);
        }

        public override Course Update(Course tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }

        public Course Update(Course tModel, Course tModelFrom)
        {
            tModel.Name = tModelFrom.Name;
            tModel.SubCourse = tModelFrom.SubCourse;
            tModel.Group = tModelFrom.Group;
            tModel.Level = tModelFrom.Level;
            tModel.FirstDate = tModelFrom.FirstDate;
            tModel.LastDate = tModelFrom.LastDate;
            tModel.Info = tModelFrom.Info;

            return this.Update(tModel);
        }

        public void LinkBook(Course tModel, Book book)
        {
            this.dbContext.Set<CourseBook>().Add(new CourseBook() { CourseId = tModel.Id, BookId = book.Id });
        }

        public void LinkBooks(Course tModel, IEnumerable<Book> books)
        {
            this.dbContext.Set<CourseBook>().AddRange(books.Select(b => new CourseBook() { CourseId = tModel.Id, BookId = b.Id }));
        }
    }
}
