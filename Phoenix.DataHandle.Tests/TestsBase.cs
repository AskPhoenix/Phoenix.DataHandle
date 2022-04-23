using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phoenix.DataHandle.Main.Models;
using System;

namespace Phoenix.DataHandle.Tests
{
    public abstract class TestsBase : IDisposable
    {
        protected readonly IConfiguration _configuration;
        protected readonly PhoenixContext _phoenixContext;

        public TestsBase()
        {
            // Attention to the json file path if the derived class is located in a subfolder

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string phoenixConnection = _configuration.GetConnectionString("PhoenixConnection");
            var dbBuilder = new DbContextOptionsBuilder<PhoenixContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(phoenixConnection);

            _phoenixContext = new PhoenixContext(dbBuilder.Options);
        }

        public virtual void Dispose()
        {
            _phoenixContext.Dispose();
        }
    }
}
