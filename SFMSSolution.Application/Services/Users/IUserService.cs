using SFMSSolution.Application.DataTransferObjects.Admin.Request;
using SFMSSolution.Application.DataTransferObjects.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Application.DataTransferObjects.User.Request;
using Microsoft.AspNetCore.Identity;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Application.DataTransferObjects.User;
using SFMSSolution.Response;
using System.Security.Claims;

namespace SFMSSolution.Application.Services.Admin
{
    public interface IUserService
    {
        Task<(List<UserResponseDto> Users, int TotalCount)> GetAllUsersAsync(
           int pageNumber,
           int pageSize,
           string? fullName = null,
           string? email = null,
           string? phone = null,
           EntityStatus? status = null,
           string? role = null);
        Task<UserDto?> GetUserByIdAsync(Guid userId);
        Task<bool> UpdateUserAsync(Guid userId, UpdateUserRequestDto request);
        Task<bool> UpdateAccountAsync(ChangeUserRoleRequestDto request);
        Task<UserDto> GetUserProfileAsync();
        Task<bool> DisableUserAsync(Guid userId);
        Task<bool> ActivateUserAsync(Guid userId);
        Task<ApiResponse<string>> ChangePasswordAsync(ChangePasswordRequestDto request);
        Task<bool> ChangeEmailAsync(ChangeEmailRequestDto request);
        Task<bool> CheckPasswordAsync(CheckPasswordRequestDto request);
    }
}
