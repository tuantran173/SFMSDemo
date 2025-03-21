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
        public string AvatarUrl { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public string Address { get; set; } = string.Empty;
        public Guid RoleId { get; set; }  // Khóa ngoại đến Role
        public Role Role { get; set; }  // Navigation Property
        public ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
    }
}
