using SFMSSolution.Application.DataTransferObjects.Admin.Request;
using SFMSSolution.Application.DataTransferObjects.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.Admin
{
    public interface IUserService
    {
        Task<(List<UserResponseDto> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<UserResponseDto?> GetUserByIdAsync(Guid userId);
        Task<bool> UpdateUserAsync(Guid userId, UpdateUserRequestDto request);
        Task<bool> ChangeUserRoleAsync(ChangeUserRoleRequestDto request);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
