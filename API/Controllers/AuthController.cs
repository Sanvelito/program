using API.Models;
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
        public async Task<ActionResult<User>> Registor(UserDto request)
        {
            var result = await _userService.Registor(request);
            if(result is  null)
            {
                return BadRequest("Username already taken");
            }
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var result = await _userService.Login(request);
            if(result is "unf")
            {
                return BadRequest("User not found");
            }

            if(result is "wp")
            {
                return BadRequest("Wrong password");
            }

            var newRefreshToken = _userService.GenerateRefreshToken();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            await _userService.SetRefreshToken(newRefreshToken);

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result =  await _userService.RefreshToken(refreshToken);
            if (result is "irt")
            {
                return Unauthorized("Invalid refresh token");
            }
            else if(result is "te")
            {
                return Unauthorized("Token Expired");
            }

            
            var newRefreshToken = _userService.GenerateRefreshToken();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            await _userService.SetRefreshToken(newRefreshToken);
            return Ok(result);
        }
        [HttpGet, Authorize]
        public ActionResult<object> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }











    }
}
