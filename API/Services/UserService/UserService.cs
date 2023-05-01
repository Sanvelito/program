using API.Controllers;
using API.Models;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;

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
        public static User staticUser = new User();
        


        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public async Task<User> Registor(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == request.Username);
            if (user.Username == request.Username){
                return null;
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User newUser = new User();
            newUser.Username = request.Username;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.Role = "user";
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<string> Login(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == request.Username);
            if (user is null)
            {
                return "unf";
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return "wp";
            }
            
            staticUser.Username = user.Username;
            string token = CreateToken(user);
            return token;
        }

        public async Task<string> RefreshToken(string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == staticUser.Username);
            if (!user.RefreshToken.Equals(refreshToken))
            {
                return "irt";
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return "te";
            }

            string token = CreateToken(user);

            return token;
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

        public async Task SetRefreshToken(RefreshToken newRefreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == staticUser.Username);
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


        #endregion

    }
}
