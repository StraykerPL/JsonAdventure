using JsonAdventure.Core.Models;
using JsonAdventure.Persistance.DbContexts;
using JsonAdventure.Persistance.Repositories.Interfaces;

namespace JsonAdventure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JsonAdventureDbContext _dbContext;

        public UserRepository(JsonAdventureDbContext context)
        {
            _dbContext = context;
        }

        public void AddUser(User newUser)
        {
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public void EditUser(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            return _dbContext.Users.Find(id) ?? throw new Exception();
        }
    }
}