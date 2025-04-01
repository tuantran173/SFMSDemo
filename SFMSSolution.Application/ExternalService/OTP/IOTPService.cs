using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.ExternalService.OTP
{
    public interface IOTPService
    {
        string GenerateOTP();
        void SaveOTP(string email, string otp);
        bool VerifyOTP(string email, string otp);
    }
}
