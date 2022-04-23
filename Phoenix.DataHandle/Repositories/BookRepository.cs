using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class BookRepository : Repository<Book>
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

        public Book? FindUnique(string name)
        {
            return FindUnique(GetUniqueExpression(name));
        }

        public Book? FindUnique(IBook book)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            return FindUnique(book.Name);
        }

        public async Task<Book?> FindUniqueAsync(string name,
            CancellationToken cancellationToken = default)
        {
            return await FindUniqueAsync(GetUniqueExpression(name),
                cancellationToken);
        }

        public async Task<Book?> FindUniqueAsync(IBook book,
            CancellationToken cancellationToken = default)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            return await FindUniqueAsync(book.Name,
                cancellationToken);
        }

        #endregion
    }
}
