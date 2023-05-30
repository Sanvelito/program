using API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto request)
        {
            var result = await _userService.Register(request);
            if(result is  null)
            {
                return BadRequest("Username already taken");
            }
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginDto>> Login(UserDto request)
        {
            var result = await _userService.Login(request);
            if(result.status is "unf")
            {
                return BadRequest("User not found");
            }

            if(result.status is "wp")
            {
                return BadRequest("Wrong password");
            }

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(string refreshToken)
        {
            var result =  await _userService.RefreshToken(refreshToken);
            if (result is "irt")
            {
                return Unauthorized("Invalid refresh token");
            }
            else if(result is "te")
            {
                return Unauthorized("Token Expired");
            }

            return Ok(result);
        }

        [HttpPost("check-auth")]
        public async Task<ActionResult<LoginDto>> CheckAuth(string request)
        {
            var result = await _userService.CheckAuth(request);
            return Ok(result);
        }
    }
}
