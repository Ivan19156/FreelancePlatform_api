﻿// DTOs/Users/RegisterUserDto.cs
namespace FreelancePlatform.Core.DTOs.Users
{
    public class RegisterUserDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}

