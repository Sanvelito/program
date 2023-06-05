using API.Models.Dto;
using Azure.Core;
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

        [HttpGet, Authorize]
        public async Task<ActionResult<UserInfoDto>> GetMyInfo()
        {
            var result = await _userService.GetMyInfo();
            if (result is null)
                return NotFound("User not found.");
            return Ok(result);
        }
        [HttpPost("update-myinfo"), Authorize]
        public async Task<ActionResult<string>> UpdateUserInfo(UserInfoDto request)
        {
            var result = await _userService.UpdateUserInfo(request);
            if (result is null)
                return NotFound("User not found.");
            return Ok(result);
        }

        //get managers for admin page
        [HttpGet("get-all-managers"), Authorize(Roles = "admin")]
        public async Task<ActionResult<List<UserInfoDto>>> GetAllManagers()
        {
            var result = await _userService.GetAllManagers();
            if (result is null)
                return NotFound("Something gone wrong");
            return Ok(result);
        }
        [HttpPost("add-new-manager"), Authorize(Roles = "admin")]
        public async Task<ActionResult<string>> AddNewManager(UserInfoDto request)
        {
            var result = await _userService.AddNewManager(request);
            if (result is null)
                return NotFound("Something gone wrong");
            return Ok(result);
        }
        [HttpPost("update-manager"), Authorize(Roles = "admin")]
        public async Task<ActionResult<string>> UpdateManager(UserInfoDto request)
        {
            var result = await _userService.UpdateManager(request);
            if (result is null)
                return NotFound("Something gone wrong");
            return Ok(result);
        }
        [HttpDelete("delete-manager"), Authorize(Roles = "admin")]
        public async Task<ActionResult<string>> DeleteManager(UserInfoDto request)
        {
            var result = await _userService.DeleteManager(request);
            if (result is null)
                return NotFound("Something gone wrong");
            return Ok(result);
        }
    }
}
