using FavoriteService.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace FavoriteService.Extension
{
    public static class MongoDbExtensions
    {
        public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
         IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            ConventionRegistry.Register("Camel Case", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);
            services.AddSingleton(s =>
            {
                return new MongoClient(s
                    .GetRequiredService<IOptions<MongoDbSettings>>().Value.ConnectionString)
                    .GetDatabase(s.GetRequiredService<IOptions<MongoDbSettings>>().Value.Database);
            });

            return services;
        }

        public static IMongoCollection<T> Collection<T>(this IMongoDatabase database)
        {
            return database.GetCollection<T>
                (nameof(T).ToLowerInvariant());
        }
    }
}
