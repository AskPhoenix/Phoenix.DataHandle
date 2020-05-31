using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Talagozis.Logging;
using Talagozis.Logging.ColoredConsole;
using Talagozis.Logging.File;

namespace Phoenix.WordPress.Puller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostBuilderContext, serviceCollection) =>
                {
                    serviceCollection.AddLoggerBackgroundService();

                    serviceCollection.TryAddSingleton<PullerBackgroundQueue>();
                    serviceCollection.AddHostedService<PullerBackgroundService>();
                    serviceCollection.AddHostedService<PullerWorker>();

                    serviceCollection.AddDbContext<PhoenixContext>(options 
                        => options.UseSqlServer(hostBuilderContext.Configuration.GetConnectionString("PhoenixConnection")));
                })
                .ConfigureLogging((hostBuilderContext, loggingBuilder) =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConfiguration(hostBuilderContext.Configuration.GetSection("Logging"));
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);

                    loggingBuilder.AddFile(a =>
                    {
                        a.folderPath = Path.Combine(Directory.GetCurrentDirectory(), "../logs/puller/");
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
                    loggingBuilder.AddColoredConsole(hostBuilderContext.Configuration.GetSection("Logging:ColoredConsole"));
                })
                .UseWindowsService();
    }
}
