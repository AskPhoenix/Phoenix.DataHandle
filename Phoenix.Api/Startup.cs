using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phoenix.Api.App_Plugins;
using Phoenix.DataHandle.Main.Models;
using Talagozis.AspNetCore.Services.TokenAuthentication;

namespace Phoenix.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this._configuration = configuration;
            this._env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PhoenixContext>(options => options.UseLazyLoadingProxies().UseSqlServer(this._configuration.GetConnectionString("PhoenixConnection")));
            
            services.AddTokenAuthentication<UserManagementService>(this._configuration);

            services.AddCors();

            services.AddHttpsRedirection(options => options.HttpsPort = 443);
            
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    //options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                    //{
                    //    NamingStrategy = new Newtonsoft.Json.Serialization.DefaultNamingStrategy()
                    //};
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(a => a.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
