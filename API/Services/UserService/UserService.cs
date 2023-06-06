using API.Controllers;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using API.Models.Dto;
using API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        public IHttpContextAccessor _httpContextAccessor { get; }
        public DataContext _context { get; }

        public UserService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        


        public async Task<UserInfoDto> GetMyInfo()
        {
            var Username = string.Empty;
            if (_httpContextAccessor != null)
            {
                Username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == Username);
            return new UserInfoDto { Username = user.Username, FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber, Role = user.Role, Manager = user.Manager };
        }
        
        public async Task<User> Register(RegisterDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == request.Username);
            if(user != null && user.Username == request.Username)
            {
                return null;
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new User();
            newUser.Username = request.Username;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.FirstName = request.FirstName;
            newUser.LastName = request.LastName;
            newUser.PhoneNumber = request.PhoneNumber;
            newUser.Role = "user";
            
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<LoginDto> Login(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == request.Username);
            if (user is null)
            {
                return new LoginDto {status = "unf"};
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new LoginDto { status = "wp" };
            }

            string accessToken = CreateToken(user);
            var newToken = GenerateRefreshToken();
            string refreshToken = newToken.Token;

            await SetRefreshToken(request.Username,newToken);

            return new LoginDto { accessToken = accessToken, refreshToken = refreshToken, role = user.Role, status = user.Manager };
        }

        public async Task<string> RefreshToken(string refreshTokenInSecure)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.RefreshToken == refreshTokenInSecure);
            if (!user.RefreshToken.Equals(refreshTokenInSecure))
            {
                return "irt";
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return "te";
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            await SetRefreshToken(user.Username,newRefreshToken);

            return token;
        }

        public async Task<LoginDto> CheckAuth(string refreshTokenInSecure)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.RefreshToken == refreshTokenInSecure);
            if (user is null)
            {
                return new LoginDto { role = "notAuth" };
            }
            if (user.TokenExpires < DateTime.Now)
            {
                return new LoginDto { role = "notAuth" };
            }

            string accessToken = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            string refreshToken = newRefreshToken.Token;
            await SetRefreshToken(user.Username, newRefreshToken);

            return new LoginDto { accessToken = accessToken, refreshToken = refreshToken, role = user.Role, status = user.Manager };
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        public async Task SetRefreshToken(string username,RefreshToken newRefreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == username);
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            await _context.SaveChangesAsync();
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        #region UserController

        public async Task<string> UpdateUserInfo(UserInfoDto request)
        {
            var Username = string.Empty;
            if (_httpContextAccessor != null)
            {
                Username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == Username);
            if (user is null)
                return null;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            if(request.Password != string.Empty) 
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;
            }
            await _context.SaveChangesAsync();
            return "success";
        }
        #endregion


        #region Admin manager page

        public async Task<List<UserInfoDto>> GetAllManagers()
        {
            var managers = await _context.Users.ToListAsync();
            var result = new List<UserInfoDto>();

            foreach (var manager in managers)
            {
                var userInfoDto = new UserInfoDto
                {
                    Username = manager.Username,
                    FirstName = manager.FirstName,
                    LastName = manager.LastName,
                    PhoneNumber = manager.PhoneNumber,
                    Role = manager.Role,
                    Manager = manager.Manager
                };
                if(manager.Manager != string.Empty)
                {
                    result.Add(userInfoDto);
                }
                
            }
            return result;
        }

        public async Task<string> DeleteManager(UserInfoDto dto)
        {
            var manager = await _context.Users.FirstOrDefaultAsync(cs => cs.Username == dto.Username);
            if (manager is null)
                return "Manager not found";
            _context.Users.Remove(manager);
            await _context.SaveChangesAsync();
            return "Success";
        }
        public async Task<string> UpdateManager(UserInfoDto dto)
        {
            var manager = await _context.Users.FirstOrDefaultAsync(cs => cs.Username == dto.Username);
            if (manager is null)
                return "Manager not found";
            
            manager.FirstName = dto.FirstName; 
            manager.LastName = dto.LastName;
            manager.PhoneNumber = dto.PhoneNumber;
            manager.Manager = dto.Manager;

            return "Success";
        }
        public async Task<string> AddNewManager(UserInfoDto dto)
        {
            var manager = await _context.Users.FirstOrDefaultAsync(cs => cs.Username == dto.Username);
            if (manager != null)
                return "Username already used";
            User newManager = new User();
            newManager.Username = dto.Username;
            newManager.FirstName = dto.FirstName;
            newManager.LastName = dto.LastName;
            newManager.PhoneNumber = dto.PhoneNumber;
            newManager.Role = "manager";
            newManager.Manager = dto.Manager;

            //password hashing
            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            newManager.PasswordSalt = passwordSalt;
            newManager.PasswordHash = passwordHash;

            _context.Users.Add(newManager);
            await _context.SaveChangesAsync();
            return "Success";
        }



        #endregion
    }
}
