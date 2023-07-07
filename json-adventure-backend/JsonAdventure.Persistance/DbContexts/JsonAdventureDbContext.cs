using JsonAdventure.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JsonAdventure.Persistance.DbContexts
{
    public class JsonAdventureDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public JsonAdventureDbContext(DbContextOptions<JsonAdventureDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }
    }
}