using API.Models.Dto;

namespace API.Services.UserService
{
    public interface IUserService
    {
        string GetMyName();
        Task<User> Register(RegisterDto request);
        Task<LoginDto> Login(UserDto request);
        Task<string> RefreshToken(string accessToken, string refreshToken);
        Task<LoginDto> CheckAuth(string refreshTokenInSecure);


        Task<List<User>> GetAllUsers();
        Task<User> GetSingleUser(int id);
        Task<List<User>> UpdateUser(int id,UserDto user);
        Task<List<User>> DeleteUser(int id);
    }
}
