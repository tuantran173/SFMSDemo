using SFMSSolution.Domain.Enums;
using System;

namespace SFMSSolution.Application.DataTransferObjects.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime? Birthday { set; get; } = DateTime.MinValue;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
