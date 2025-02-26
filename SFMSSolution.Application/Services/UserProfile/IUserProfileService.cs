using SFMSSolution.Application.DataTransferObjects.User;
using SFMSSolution.Application.DataTransferObjects.User.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.UserProfile
{

    public interface IUserProfileService
    {
        /// <summary>
        /// Lấy thông tin hồ sơ người dùng.
        /// </summary>
        /// <param name="userId">ID của người dùng</param>
        /// <returns>Thông tin hồ sơ của người dùng</returns>
        Task<ProfileResponseDto?> GetUserProfileAsync(Guid userId);

        /// <summary>
        /// Cập nhật hồ sơ người dùng.
        /// </summary>
        /// <param name="userId">ID của người dùng</param>
        /// <param name="request">Dữ liệu cần cập nhật</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại</returns>
        Task<bool> UpdateUserProfileAsync(Guid userId, UpdateProfileRequestDto request);

        /// <summary>
        /// Đổi mật khẩu người dùng.
        /// </summary>
        /// <param name="userId">ID của người dùng</param>
        /// <param name="request">Dữ liệu chứa mật khẩu cũ và mật khẩu mới</param>
        /// <returns>True nếu đổi mật khẩu thành công, False nếu thất bại</returns>
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequestDto request);
    }


}
