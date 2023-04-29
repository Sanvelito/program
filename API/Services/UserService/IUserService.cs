namespace API.Services.UserService
{
    public interface IUserService
    {
        string GetMyName();
        Task<User> Registor(UserDto request);
        Task<string> Login(UserDto request);
        string RefreshToken(string refreshToken);
        RefreshToken GenerateRefreshToken();
        void SetRefreshToken(RefreshToken newRefreshToken);
    }
}
