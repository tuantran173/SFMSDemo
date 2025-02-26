using SFMSSolution.Domain.Entities.Base;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class User: BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public bool Deleted { get; set; } = false;
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public string ResetPasswordToken { get; set; } = string.Empty;
        public DateTime? ResetPasswordTokenExpiry { get; set; }
        // Navigation property cho mối quan hệ nhiều-nhiều với Role thông qua bảng UserRole
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        
    }
}
