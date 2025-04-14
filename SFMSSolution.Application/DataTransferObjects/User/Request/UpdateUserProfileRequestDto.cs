using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.User.Request
{
    public class UpdateUserProfileRequestDto
    {
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public Gender? Gender { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string? AvatarUrl { get; set; }
        public EntityStatus? Status { get; set; }
    }
}
