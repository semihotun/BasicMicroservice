using Insfrastructure.Context;
using Insfrastructure.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace PageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(GetConfiguration(), args);

            host.MigrateDbContext<PageDbContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<PageDbContextSeed>>();

                var dbContextSeeder = new PageDbContextSeed();
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
            .Build();

        static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
