using IdentityService.Context;
using IdentityService.Extension;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(GetConfiguration(), args);
        
            Log.Logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(SerilogConfiguration)
                 .CreateLogger();
            Log.Debug("sss");


            host.MigrateDbContext<IdentityContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<IdentityContextSeed>>();

                var dbContextSeeder = new IdentityContextSeed();
                dbContextSeeder.SeedAsync(context, logger).Wait();

            });
            host.Run();
        }

        static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
           .UseDefaultServiceProvider((context, options) =>
           {
               options.ValidateOnBuild = false;
               options.ValidateScopes = false;
           })
            .ConfigureAppConfiguration(p => p.AddConfiguration(configuration))
         
            .UseStartup<Startup>()
            .UseSerilog()
            .Build();

        static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
        static IConfiguration SerilogConfiguration
        {
            get
            {
                return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"serilog.json", optional: false)
                    .AddEnvironmentVariables()
                    .Build();
            }
        }

    }
}
