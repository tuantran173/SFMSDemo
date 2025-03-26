using Microsoft.AspNetCore.Identity;
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
    public class User: IdentityUser<Guid>
    {
        public string? FullName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string? Address { get; set; } = string.Empty;
        public DateTime? Birthday { set; get; } = DateTime.MinValue;
        public new string? Email { get; set; }
        public string? AvatarUrl { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        
    }
}
