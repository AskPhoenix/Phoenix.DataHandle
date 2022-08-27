using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.Verification.DataHandle.Base
{
    public abstract class ContextTestsBase : ConfigurationTestsBase, IDisposable
    {
        protected PhoenixContext _phoenixContext;
        protected ApplicationContext _applicationContext;

        public ContextTestsBase()
            : base()
        {
            string phoenixConnection = _configuration.GetConnectionString("PhoenixConnection");

            _phoenixContext = new PhoenixContext(new DbContextOptionsBuilder<PhoenixContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(phoenixConnection)
                .Options);

            _applicationContext = new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(phoenixConnection)
                .Options);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_phoenixContext is not null)
                {
                    _phoenixContext.Dispose();
                    _phoenixContext = null!;
                }

                if(_applicationContext is not null)
                {
                    _applicationContext.Dispose();
                    _applicationContext = null!;
                }
            }
        }
    }
}
