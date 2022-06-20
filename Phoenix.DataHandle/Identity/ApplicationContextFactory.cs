using Microsoft.EntityFrameworkCore.Design;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args) => new(new());
    }
}
