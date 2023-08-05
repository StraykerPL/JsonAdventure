using JsonAdventure.Core.Models;

namespace JsonAdventure.Application.Services.Interfaces
{
    public interface IUserService
    {
        User GetUser(string name);

        User GetUser(int id);

        void AddUser(User newUser);

        void EditUser(User updatedUser);

        void DeleteUser(int id);
    }
}