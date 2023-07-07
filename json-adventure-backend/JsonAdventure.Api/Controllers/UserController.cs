using JsonAdventure.Application.Services.Interfaces;
using JsonAdventure.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace JsonAdventure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hejo! :)");
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userService.GetUser(id);
        }

        [HttpPost]
        public void Post(string name)
        {
            _userService.AddUser(name);
        }

        [HttpPut("{id}")]
        public void Put(string newValue)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.DeleteUser(id);
        }
    }
}