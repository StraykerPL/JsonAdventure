using JsonAdventure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JsonAdventure.Persistance.DependencyInjection
{
    public static class AddMySqlDbExtension
    {
        public static IServiceCollection AddMySqlDb(this IServiceCollection services, string connectionString, string version)
        {
            var versionObject = new MySqlServerVersion(new Version(version));
            services.AddDbContext<JsonAdventureDbContext>(options => options.UseMySql(connectionString, versionObject));

            return services;
        }
    }
}