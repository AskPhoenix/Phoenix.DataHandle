using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JustScheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.Scheduler.App_Plugins.Services;
using Talagozis.Logging;
using Talagozis.Logging.ColoredConsole;
using Talagozis.Logging.File;

namespace Phoenix.Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLoggerBackgroundService();

                    services.AddDbContext<PhoenixContext>(options =>
                    {
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("PhoenixConnection"));
                    });

                    services.AddJustScheduler(a =>
                    {
                        a.WithRunner<LectureService>()
                            .ScheduleOnce()
                            .ScheduleEvery(TimeSpan.FromSeconds(60 * 30))
                            .Build();
                    });

                }).ConfigureLogging((hostBuilderContext, loggingBuilder) =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConfiguration(hostBuilderContext.Configuration.GetSection("Logging"));
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);

                    loggingBuilder.AddColoredConsole(hostBuilderContext.Configuration.GetSection("Logging:ColoredConsole"));

                    loggingBuilder.AddFile(a =>
                    {
                        a.folderPath = Path.Combine(Directory.GetCurrentDirectory(), "../logs/api/");
                        a.Add(new FileLoggerConfiguration
                        {
                            logLevel = LogLevel.Warning
                        });
                        a.Add(new FileLoggerConfiguration
                        {
                            logLevel = LogLevel.Error
                        });
                        a.Add(new FileLoggerConfiguration
                        {
                            logLevel = LogLevel.Critical
                        });
                    });
                });
    }
}
