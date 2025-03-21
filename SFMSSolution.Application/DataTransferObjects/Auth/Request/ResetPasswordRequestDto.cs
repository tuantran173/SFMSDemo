using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Auth.Request
{
    public class ResetPasswordRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string OTP { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
