using Insfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Insfrastructure.Extensions
{
    public static class DbContextRegistiration
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<PageDbContext>(option =>
            {
                option.UseSqlServer(configuration["ConnectionString"],
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 15,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            });
            var optionBuilder=new DbContextOptionsBuilder<PageDbContext>()
                .UseSqlServer(configuration["ConnectionString"]);

            using (var ctx = new PageDbContext(optionBuilder.Options, null))
            {
                if (!ctx.Database.EnsureCreated()) {
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
