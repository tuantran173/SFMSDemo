using SFMSSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Domain.Entities
{
    public class UserToken : BaseEntity
    {
        // Khóa ngoại liên kết đến User
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Giá trị của token (ví dụ refresh token, OTP, reset password, …)
        [Required]
        [StringLength(1000)]
        public string Token { get; set; } = string.Empty;

        // Thời gian hết hạn của token
        [Required]
        public DateTime Expiry { get; set; }

        // Loại token, ví dụ: "Refresh", "ResetPasswordOTP", ...
        [Required]
        [StringLength(50)]
        public string TokenType { get; set; } = string.Empty;
    }
}
