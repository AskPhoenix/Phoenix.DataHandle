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
                .ConfigureLogging(loggingBuilder =>
                {
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
#if DEBUG
                    loggingBuilder.AddColoredConsole(a =>
                    {
                        a.Add(new ColoredConsoleLoggerConfiguration
                        {
                            color = ConsoleColor.Red,
                            logLevel = LogLevel.Error,
                        });
                        a.Add(new ColoredConsoleLoggerConfiguration
                        {
                            color = ConsoleColor.Yellow,
                            logLevel = LogLevel.Warning,
                        });
                        a.Add(new ColoredConsoleLoggerConfiguration
                        {
                            namespacePrefix = "Phoenix",
                            color = ConsoleColor.Green,
                            logLevel = LogLevel.Information,
                        });
                        a.Add(new ColoredConsoleLoggerConfiguration
                        {
                            namespacePrefix = "Phoenix",
                            color = ConsoleColor.DarkCyan,
                            logLevel = LogLevel.Debug,
                        });
                        a.Add(new ColoredConsoleLoggerConfiguration
                        {
                            namespacePrefix = "Phoenix",
                            color = ConsoleColor.White,
                            logLevel = LogLevel.Trace,
                        });
                    });
#endif
                });
    }
}
