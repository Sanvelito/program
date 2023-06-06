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

        [HttpGet, Authorize]
        public async Task<ActionResult<UserInfoDto>> GetMyInfo()
        {
            var result = await _userService.GetMyInfo();
            if (result is null)
                return NotFound("Пользователь не найден!");
            return Ok(result);
        }
        [HttpPost("update-myinfo"), Authorize]
        public async Task<ActionResult<string>> UpdateUserInfo(UserInfoDto request)
        {
            var result = await _userService.UpdateUserInfo(request);
            if (result is null)
                return NotFound("Пользователь не найден!");
            return Ok(result);
        }

        //get managers for admin page
        [HttpGet("get-all-managers"), Authorize(Roles = "admin")]
        public async Task<ActionResult<List<UserInfoDto>>> GetAllManagers()
        {
            var result = await _userService.GetAllManagers();
            if (result is null)
                return NotFound("Что-то пошло не так!");
            return Ok(result);
        }
        [HttpPost("add-new-manager"), Authorize(Roles = "admin")]
        public async Task<ActionResult<string>> AddNewManager(UserInfoDto request)
        {
            var result = await _userService.AddNewManager(request);
            if (result is null)
                return NotFound("Что-то пошло не так!");
            return Ok(result);
        }
        [HttpPost("update-manager"), Authorize(Roles = "admin")]
        public async Task<ActionResult<string>> UpdateManager(UserInfoDto request)
        {
            var result = await _userService.UpdateManager(request);
            if (result is null)
                return NotFound("Что-то пошло не так!");
            return Ok(result);
        }
        [HttpDelete("delete-manager"), Authorize(Roles = "admin")]
        public async Task<ActionResult<string>> DeleteManager(UserInfoDto request)
        {
            var result = await _userService.DeleteManager(request);
            if (result is null)
                return NotFound("Что-то пошло не так!");
            return Ok(result);
        }
    }
}
