using JsonAdventure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JsonAdventure.Persistance.DependencyInjection
{
    public static class AddMySqlDbExtension
    {
        public static IServiceCollection AddMySqlDb(this IServiceCollection services)
        {
            var version = new MySqlServerVersion(new Version(10, 4, 27));
            services.AddDbContext<JsonAdventureDbContext>(options => options.UseMySql("", version));

            return services;
        }
    }
}