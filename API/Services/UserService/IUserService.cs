using API.Models.Dto;

namespace API.Services.UserService
{
    public interface IUserService
    {
        Task<UserInfoDto> GetMyInfo();
        Task<UserInfoDto> UpdateUserInfo(UserInfoDto request);
        Task<User> Register(RegisterDto request);
        Task<LoginDto> Login(UserDto request);
        Task<string> RefreshToken(string refreshToken);
        Task<LoginDto> CheckAuth(string refreshTokenInSecure);


        Task<List<User>> GetAllUsers();
        Task<User> GetSingleUser(int id);
        Task<List<User>> UpdateUser(int id,UserDto user);
        Task<List<User>> DeleteUser(int id);
    }
}
