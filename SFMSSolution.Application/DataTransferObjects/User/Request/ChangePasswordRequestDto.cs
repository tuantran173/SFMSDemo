using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.User.Request
{
    public class ChangePasswordRequestDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmedPassword { get; set; } = string.Empty;
    }
}
