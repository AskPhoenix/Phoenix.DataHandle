using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Models;
using System;

namespace Phoenix.DataHandle.Tests
{
    public abstract class TestsBase : IDisposable
    {
        protected readonly IConfiguration _configuration;
        protected readonly PhoenixContext _phoenixContext;
        protected readonly ApplicationDbContext _applicationContext;

        public TestsBase()
        {
            // Attention to the json file path if the derived class is located in a subfolder
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string phoenixConnection = _configuration.GetConnectionString("PhoenixConnection");
            
            _phoenixContext = new PhoenixContext(new DbContextOptionsBuilder<PhoenixContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(phoenixConnection)
                .Options);

            _applicationContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseLazyLoadingProxies()
                .UseSqlServer(phoenixConnection)
                .Options);
        }

        public virtual void Dispose()
        {
            _phoenixContext.Dispose();
        }
    }
}
