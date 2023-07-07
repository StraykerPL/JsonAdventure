using JsonAdventure.Application.Services.Interfaces;
using JsonAdventure.Core.Models;
using JsonAdventure.Persistance.Repositories.Interfaces;

namespace JsonAdventure.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(string name)
        {
            return _userRepository.GetUser(name);
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public void AddUser(string name)
        {
            var newUser = new User
            {
                Name = name
            };

            _userRepository.AddUser(newUser);
        }

        public void EditUser(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}