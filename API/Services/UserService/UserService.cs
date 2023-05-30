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
            return new UserInfoDto { Username = user.Username, FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber, Role = user.Role };
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

            return new LoginDto { accessToken = accessToken, refreshToken = refreshToken, role = user.Role };
        }

        public async Task<string> RefreshToken(string refreshTokenInSecure)
        {
            //var jwt = new JwtSecurityToken(accessToken);
            //string userName = jwt.Claims.First(c => c.Type == "name").Value;

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
                return new LoginDto { status = "notAuth" };
            }
            if (user.TokenExpires < DateTime.Now)
            {
                return new LoginDto { status = "notAuth" };
            }

            string accessToken = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            string refreshToken = newRefreshToken.Token;
            await SetRefreshToken(user.Username, newRefreshToken);

            return new LoginDto { accessToken = accessToken, refreshToken = refreshToken, role = user.Role, status = "Auth" };
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
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        #region UserController

        public async Task<List<User>> GetAllUsers()
        {
            var user = await _context.Users.ToListAsync();
            return user;
        }
        public async Task<User> GetSingleUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return null;
            return user;
        }
        public async Task<List<User>> UpdateUser(int id, UserDto request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return null;
            user.Username = request.Username;
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Role = "user";

            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();

        }
        public async Task<List<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return null;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();
        }

        public async Task<UserInfoDto> UpdateUserInfo(UserInfoDto request)
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
            if(request.Password != null) 
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;
            }
            await _context.SaveChangesAsync();
            return new UserInfoDto { Username = user.Username, FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber, Role = user.Role };
        }
        #endregion

    }
}
