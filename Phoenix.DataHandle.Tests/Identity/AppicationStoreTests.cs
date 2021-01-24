using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Identity;
using Xunit;

namespace Phoenix.DataHandle.Tests.Repositories
{
    public class AppicationStoreTests : IDisposable
    {
        //private const string CONNECTION_STRING = "Server=.;Initial Catalog=PhoenixDB;Persist Security Info=False;User ID=sa;Password=root;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
        private const string CONNECTION_STRING = "Server=tcp:askphoenix.database.windows.net,1433;Initial Catalog=NuageDB;Persist Security Info=False;User ID=phoenix;Password=20Ph0eniX20!;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private readonly ApplicationDbContext _applicationDbContext;

        private const string PHONE_NUMBER = "6978348753";
        private const string PROVIDER_KEY = "3719105304804966";

        public AppicationStoreTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            this._applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);
        }

        public void Dispose()
        {
            this._applicationDbContext.Dispose();
        }

        [Fact]
        public async void FindByPhoneNumber()
        {
            ApplicationStore applicationStore = new ApplicationStore(this._applicationDbContext);

            ApplicationUser applicationUser = await applicationStore.FindByPhoneNumberAsync(PHONE_NUMBER, CancellationToken.None);

            Assert.NotNull(applicationUser);
        }

        [Fact]
        public async void FindByProviderKey_Facebook()
        {
            ApplicationStore applicationStore = new ApplicationStore(this._applicationDbContext);

            ApplicationUser applicationUser = await applicationStore.FindByProviderKeyAsync(Main.LoginProvider.Facebook, PROVIDER_KEY, CancellationToken.None);

            Assert.NotNull(applicationUser);
        }

        [Fact]
        public async void GetRolesAsync()
        {
            ApplicationStore applicationStore = new ApplicationStore(this._applicationDbContext);

            ApplicationUser applicationUser = await applicationStore.FindByPhoneNumberAsync(PHONE_NUMBER, CancellationToken.None);

            IList<string> roles = await applicationStore.GetRolesAsync(applicationUser, CancellationToken.None);

            Assert.NotNull(roles);
        }

    }
}
