using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.User.Request
{
    public class CheckPasswordRequestDto
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; } = string.Empty;
    }
}
