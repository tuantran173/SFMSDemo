using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IUserTokenRepository
    {
        // Lấy một token theo UserId và TokenType (ví dụ: "Refresh", "ResetPasswordOTP")
        Task<UserToken?> GetTokenAsync(Guid userId, string tokenType);

        // Lấy một token theo giá trị token và TokenType
        Task<UserToken?> GetTokenAsyncByValue(string tokenValue, string tokenType);

        // Lấy danh sách các token của một user theo TokenType
        Task<List<UserToken>> GetTokensByUserIdAndTypeAsync(Guid userId, string tokenType);

        // Thêm một token mới
        Task<bool> AddAsync(UserToken token);

        // Xóa một token
        Task<bool> DeleteAsync(UserToken token);
    }
}
