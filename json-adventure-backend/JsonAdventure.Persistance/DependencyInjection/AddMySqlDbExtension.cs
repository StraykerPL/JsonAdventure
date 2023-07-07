using JsonAdventure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JsonAdventure.Persistance.DependencyInjection
{
    public static class AddMySqlDbExtension
    {
        public static IServiceCollection AddMySqlDb(
            this IServiceCollection services,
            string connectionString,
            string dbEngineVersion)
        {
            var mySqlVersionObject = new MySqlServerVersion(dbEngineVersion);
            services.AddDbContext<JsonAdventureDbContext>(
                options => options.UseMySql(connectionString, mySqlVersionObject)
                );

            return services;
        }
    }
}