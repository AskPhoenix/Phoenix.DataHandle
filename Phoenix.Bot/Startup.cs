using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phoenix.Bot.Bots;
using Phoenix.Bot.Dialogs;
using Phoenix.DataHandle.Bot.Storage;
using Phoenix.DataHandle.Models;

namespace Phoenix.Bot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            services.AddSingleton<IStorage>(new EntityFrameworkStorage(Configuration.GetConnectionString("LocalConnection")));
            services.AddSingleton(new EntityFrameworkTranscriptStore(Configuration.GetConnectionString("LocalConnection")));

            services.AddSingleton<UserState>();
            services.AddSingleton<ConversationState>();

            services.AddSingleton<MainDialog>();
            services.AddTransient<IBot, DialogBot<MainDialog>>();
            
            services.AddApplicationInsightsTelemetry();
            services.AddControllers();
            services.AddHttpsRedirection(options => options.HttpsPort = 443);

            services.AddDbContext<PhoenixContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PhoenixConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<PhoenixContext>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
