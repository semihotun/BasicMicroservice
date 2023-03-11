using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application.StartUp
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);

            return services;
        }
    }
}
