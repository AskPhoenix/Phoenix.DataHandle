using Microsoft.EntityFrameworkCore;
using System;

namespace Phoenix.DataHandle.Utilities
{
    public static class DbContextExtensions
    {
        public static bool TrySaveChanges(this DbContext context, out int numEntriesWritten)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            try
            {
                numEntriesWritten = context.SaveChanges();
                return true;
            }
            catch (Exception ex) when (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                numEntriesWritten = 0;
                return false;
            }
        }
    }
}
