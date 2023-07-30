using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JsonAdventure.Persistance.DbContexts
{
    public class UserIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public UserIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}