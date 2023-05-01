namespace API.Services.UserService
{
    public interface IUserService
    {
        string GetMyName();
        Task<User> Registor(UserDto request);
        Task<string> Login(UserDto request);
        Task<string> RefreshToken(string refreshToken);
        RefreshToken GenerateRefreshToken();
        Task SetRefreshToken(RefreshToken newRefreshToken);

        Task<List<User>> GetAllUsers();
        Task<User> GetSingleUser(int id);
        Task<List<User>> UpdateUser(int id,UserDto user);
        Task<List<User>> DeleteUser(int id);
    }
}
