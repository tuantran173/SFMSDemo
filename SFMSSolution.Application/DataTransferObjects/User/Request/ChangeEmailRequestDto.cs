using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.User.Request
{
    public class ChangeEmailRequestDto
    {
        public Guid UserId { get; set; }
        public string NewEmail { get; set; } = string.Empty;
    }
}
