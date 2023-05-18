using API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet("Get-all-users"), Authorize(Roles = "admin")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            var result = await _userService.GetSingleUser(id);
            if (result is null)
                return NotFound("User not found.");

            return Ok(result);
        }

        [HttpPut("{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, UserDto request)
        {
            var result = await _userService.UpdateUser(id, request);
            if (result is null)
                return NotFound("User not found.");

            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result is null)
                return NotFound("User not found.");

            return Ok(result);
        }
    }
}
