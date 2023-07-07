using JsonAdventure.Application.Services;
using JsonAdventure.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JsonAdventure.Application.DependencyInjection
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}