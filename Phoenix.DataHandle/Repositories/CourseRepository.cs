﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(PhoenixContext dbContext) : base(dbContext) { }

        public Course Update(Course tModel, Course tModelFrom)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (tModelFrom == null)
                throw new ArgumentNullException(nameof(tModelFrom));

            tModel.Name = tModelFrom.Name;
            tModel.SubCourse = tModelFrom.SubCourse;
            tModel.Group = tModelFrom.Group;
            tModel.Level = tModelFrom.Level;
            tModel.FirstDate = tModelFrom.FirstDate;
            tModel.LastDate = tModelFrom.LastDate;
            tModel.Info = tModelFrom.Info;

            return this.Update(tModel);
        }

        public void LinkBook(Course tModel, int bookId)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            this.dbContext.Set<CourseBook>().Add(new CourseBook() { CourseId = tModel.Id, BookId = bookId });
            this.dbContext.SaveChanges();
        }

        public void LinkBooks(Course tModel, IEnumerable<int> bookIds)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));
            if (bookIds == null)
                throw new ArgumentNullException(nameof(bookIds));

            this.dbContext.Set<CourseBook>().AddRange(bookIds.Select(bId => new CourseBook() { CourseId = tModel.Id, BookId = bId }));
            this.dbContext.SaveChanges();
        }

        public IEnumerable<Book> GetLinkedBooks(Course tModel)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            return this.dbContext.Set<CourseBook>().Include(cb => cb.Book).Where(cb => cb.CourseId == tModel.Id).Select(cb => cb.Book).AsEnumerable();
        }
    }
}
