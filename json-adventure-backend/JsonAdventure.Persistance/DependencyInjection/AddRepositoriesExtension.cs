using JsonAdventure.Persistance.Repositories;
using JsonAdventure.Persistance.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JsonAdventure.Persistance.DependencyInjection
{
    public static class AddRepositoriesExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}