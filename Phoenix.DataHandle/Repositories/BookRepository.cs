using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories.Extensions;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class BookRepository : Repository<Book>,
        ISetNullDeleteRule<Book>
    {
        public BookRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        public static Expression<Func<Book, bool>> GetUniqueExpression(string bookName)
        {
            if (string.IsNullOrWhiteSpace(bookName))
                throw new ArgumentNullException(nameof(bookName));

            string normName = Book.NormFunc(bookName);
            return b => b.NormalizedName == normName;
        }

        #region Find Unique

        public Task<Book?> FindUniqueAsync(string name,
            CancellationToken cancellationToken = default)
        {
            return FindUniqueAsync(GetUniqueExpression(name),
                cancellationToken);
        }

        public Task<Book?> FindUniqueAsync(IBookBase book,
            CancellationToken cancellationToken = default)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            return FindUniqueAsync(book.Name,
                cancellationToken);
        }

        #endregion

        #region Delete

        public void SetNullOnDelete(Book book)
        {
            book.Courses.Clear();
            book.Exercises.Clear();
            book.Materials.Clear();
        }

        #endregion
    }
}
