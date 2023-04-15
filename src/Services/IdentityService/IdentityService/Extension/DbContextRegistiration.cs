using IdentityService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IdentityService.Extension
{
    public static class DbContextRegistiration
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var cnn = "";
            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                cnn = configuration["LocalConnectionString"];
            }
            else
            {
                cnn = configuration["ConnectionString"];
            }
            services.AddEntityFrameworkSqlServer().AddDbContext<IdentityContext>(option =>
            {
                option.UseSqlServer(cnn,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 15,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            });
            var optionBuilder = new DbContextOptionsBuilder<IdentityContext>()
                .UseSqlServer(cnn);

            using (var ctx = new IdentityContext(optionBuilder.Options, null))
            {
                if (!ctx.Database.EnsureCreated())
                {
                    ctx.Database.EnsureCreated();
                    ctx.Database.Migrate();
                }
                else
                {
                    ctx.Database.Migrate();
                }

            }            

            return services;
        }
    }
}
