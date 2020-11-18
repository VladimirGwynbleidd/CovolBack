using log4net;
using log4net.Appender;
using log4net.Config;
using MicroKnights.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace WebApiCore
{
    public static class Program
    {
        static void Main(string[] args)
        {
            log4net.Repository.ILoggerRepository logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            foreach (IAppender appender in logRepository.GetAppenders())
            {
                var append = appender as AdoNetAppender;
                //if (appender is AdoNetAppender)
                if (append != null)
                {
                    AdoNetAppender adoNetAppender = (AdoNetAppender)appender;
                    adoNetAppender.ConnectionString = Environment.GetEnvironmentVariable("DefaultConnection");
                    adoNetAppender.ActivateOptions();
                }
            }
            BuildWebHost(args).Run();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
            .UseIISIntegration()
            .UseStartup<Startup>()
            .UseKestrel(o =>
            {
                Console.WriteLine("Inciando kestrel");
                o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(1);
                Console.WriteLine($"Request Timeout antes: {o.Limits.RequestHeadersTimeout.ToString()}");
                o.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
                Console.WriteLine($"Request Timeout despues: {o.Limits.RequestHeadersTimeout.ToString()}");
                o.Limits.MaxRequestBodySize = 10 * 1024;
                o.Limits.MinRequestBodyDataRate =
                new MinDataRate(bytesPerSecond: 100,
                gracePeriod: TimeSpan.FromSeconds(10));
                o.Limits.MinResponseDataRate =
                    new MinDataRate(bytesPerSecond: 100,
                        gracePeriod: TimeSpan.FromSeconds(10));
            })
            .Build();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureLogging((Action<WebHostBuilderContext, ILoggingBuilder>)((hostingContext, logging) =>
            {
                logging.AddConfiguration((IConfiguration)hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
                //logging.AddEventLog();
                //logging.AddEventSourceLogger();
            }))
               .UseStartup<Startup>();

    }
}
