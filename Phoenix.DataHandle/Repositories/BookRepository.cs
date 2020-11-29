using Phoenix.DataHandle.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DataHandle.Repositories
{
    public class BookRepository : Repository<Book>
    {
        public BookRepository(PhoenixContext dbContext) : base(dbContext) { }

        public IEnumerable<Book> CreateMany(IEnumerable<Book> tModels)
        {
            if (tModels == null)
                throw new ArgumentNullException(nameof(tModels));
            
            tModels = tModels.Where(b => b != null);

            this.dbContext.Set<Book>().AddRange(tModels);
            this.dbContext.SaveChanges();

            return tModels;
        }
    }
}
