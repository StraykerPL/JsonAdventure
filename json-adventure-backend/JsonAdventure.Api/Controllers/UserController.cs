using JsonAdventure.Application.Services.Interfaces;
using JsonAdventure.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JsonAdventure.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public User Get(int id)
        {
            return _userService.GetUser(id);
        }

        [AllowAnonymous]
        [HttpPost("new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public void Post([FromBody] User userData)
        {
            _userService.AddUser(userData);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public void Put(int id, string newValue)
        {
            _userService.EditUser(new User { Id = id, Name = newValue });
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public void Delete(int id)
        {
            _userService.DeleteUser(id);
        }
    }
}