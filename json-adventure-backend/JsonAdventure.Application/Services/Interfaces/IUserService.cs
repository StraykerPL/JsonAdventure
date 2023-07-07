using JsonAdventure.Core.Models;

namespace JsonAdventure.Application.Services.Interfaces
{
    public interface IUserService
    {
        User GetUser(string name);

        User GetUser(int id);

        void AddUser(string name);

        void EditUser(int id);

        void DeleteUser(int id);
    }
}