namespace API.Models
{
    public class LoginDto
    {
        public string accessToken { get; set; } = string.Empty;
        public string refreshToken { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
    }
}
