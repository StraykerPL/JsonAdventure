using JsonAdventure.Application.Services.Interfaces;
using JsonAdventure.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JsonAdventure.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userService.GetUser(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public void Post([FromBody] User userData)
        {
            _userService.AddUser(userData);
        }

        [HttpPut("{id}")]
        public void Put(int id, string newValue)
        {
            _userService.EditUser(new User { Id = id, Name = newValue });
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.DeleteUser(id);
        }
    }
}