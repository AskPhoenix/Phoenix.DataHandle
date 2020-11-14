using Microsoft.EntityFrameworkCore;
using System;

namespace Phoenix.DataHandle.Utilities
{
    public static class DbContextExtensions
    {
        public static bool TrySaveChanges(this DbContext context, out int numEntriesWritten)
        {
            try
            {
                numEntriesWritten = context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                numEntriesWritten = 0;
                return false;
            }
        }
    }
}
