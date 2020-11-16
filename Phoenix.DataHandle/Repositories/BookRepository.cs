using Phoenix.DataHandle.Main.Models;
using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Repositories
{
    public class BookRepository : Repository<Book>
    {
        public BookRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override Book Create(Book tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;

            return base.Create(tModel);
        }

        public override Book Update(Book tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }

        public IEnumerable<Book> CreateMany(IEnumerable<Book> tModels)
        {
            this.dbContext.Set<Book>().AddRange(tModels);
            this.dbContext.SaveChanges();

            return tModels;
        }
    }
}
