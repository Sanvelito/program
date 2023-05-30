﻿namespace API.Models.Dto
{
    public class UserInfoDto
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
