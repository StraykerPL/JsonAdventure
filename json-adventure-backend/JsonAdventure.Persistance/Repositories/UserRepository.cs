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
            _dbContext.Remove(_dbContext.Users.Find(id)!);
        }

        public void EditUser(int id, User newUserData)
        {
            var objectToReplace = _dbContext.Users.Find(id)!;
            objectToReplace.Name = newUserData.Name;
            _dbContext.Users.Update(objectToReplace);
            _dbContext.SaveChanges();
        }

        public User GetUser(string name)
        {
            return _dbContext.Users.Find(name)!;
        }

        public User GetUser(int id)
        {
            return _dbContext.Users.Find(id)!;
        }
    }
}