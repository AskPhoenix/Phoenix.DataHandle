using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Talagozis.AspNetCore.Services.Logger;
using Talagozis.AspNetCore.Services.Logger.ColoredConsole;
using Talagozis.AspNetCore.Services.Logger.File;

namespace Phoenix.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel((context, options) => options.AddServerHeader = false);
                })
                .ConfigureServices(serviceCollection => { serviceCollection.AddLoggerBackgroundService(); })
                .ConfigureLogging((hostingContext, loggingBuilder) =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);

                    loggingBuilder.AddFile(a =>
                    {
                        a.folderPath = Path.Combine(Directory.GetCurrentDirectory(), "../logs/");
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
                    loggingBuilder.AddColoredConsole(hostingContext.Configuration.GetSection("Logging:ColoredConsole"));

                });
    }
}
