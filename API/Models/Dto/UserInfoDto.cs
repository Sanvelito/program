namespace API.Models.Dto
{
    public class UserInfoDto
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
    }
}
