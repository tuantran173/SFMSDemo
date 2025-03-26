using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.User
{
    public class ProfileResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public DateTime? Birthday { get; set; }
        public string AvatarUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
