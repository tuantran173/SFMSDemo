using System;

namespace SFMSSolution.Application.DataTransferObjects.Auth
{
    public class AuthResponseDto
    {
        public Guid Id { get; set; } // Thêm Id để client có thể tham chiếu user
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; // Thêm Email nếu cần
    }
}