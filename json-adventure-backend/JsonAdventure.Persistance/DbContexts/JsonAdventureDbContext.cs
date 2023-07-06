using Microsoft.EntityFrameworkCore;

namespace JsonAdventure.Persistance.DbContexts
{
    public class JsonAdventureDbContext : DbContext
    {
        public JsonAdventureDbContext(DbContextOptions<JsonAdventureDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }
    }
}