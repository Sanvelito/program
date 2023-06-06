using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.UserService
{
    public interface IUserService
    {
        Task<UserInfoDto> GetMyInfo();
        Task<string> UpdateUserInfo(UserInfoDto request);

        Task<User> Register(RegisterDto request);
        Task<LoginDto> Login(UserDto request);
        Task<string> RefreshToken(string refreshToken);
        Task<LoginDto> CheckAuth(string refreshTokenInSecure);


        Task<List<UserInfoDto>> GetAllManagers();
        Task<string> AddNewManager(UserInfoDto dto);
        Task<string> DeleteManager(UserInfoDto dto);
        Task<string> UpdateManager(UserInfoDto dto);
    }
}
