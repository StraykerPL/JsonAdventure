using JsonAdventure.Persistance.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace JsonAdventure.Persistance.DependencyInjection
{
    public static class AddIdentityExtension
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<UserIdentityDbContext>();

            return services;
        }
    }
}