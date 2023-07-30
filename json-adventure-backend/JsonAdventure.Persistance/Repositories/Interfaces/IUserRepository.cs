using JsonAdventure.Core.Models;

namespace JsonAdventure.Persistance.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string name);

        User GetUser(int id);

        void AddUser(User newUser);

        void EditUser(int id, User newUserData);

        void DeleteUser(int id);
    }
}