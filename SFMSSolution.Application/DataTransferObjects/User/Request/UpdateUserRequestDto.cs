using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.User.Request
{
    public class UpdateUserRequestDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime Birthday { set; get; } = DateTime.MinValue;
        public string Email { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
