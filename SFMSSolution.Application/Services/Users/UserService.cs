using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Admin.Request;
using SFMSSolution.Application.DataTransferObjects.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Application.DataTransferObjects.User.Request;
using SFMSSolution.Application.DataTransferObjects.User;
using Microsoft.AspNetCore.Http;
using SFMSSolution.Response;
using System.Security.Claims;

namespace SFMSSolution.Application.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(List<UserResponseDto> Users, int TotalCount)> GetAllUsersAsync(
            int pageNumber,
            int pageSize,
            string? fullName = null,
            string? email = null,
            string? phone = null,
            EntityStatus? status = null,
            string? role = null)
        {
            var (users, totalCount) = await _unitOfWork.AdminRepository.GetAllUsersWithRolesAsync(
                pageNumber,
                pageSize,
                fullName,
                email,
                phone,
                status,
                role
            );

            var userDtos = new List<UserResponseDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserResponseDto>(user);
                userDto.Role = roles.FirstOrDefault() ?? string.Empty;

                if (userDto.Role != "Admin") // Bỏ qua Admin
                    userDtos.Add(userDto);
            }

            return (userDtos, totalCount);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Role = roles.FirstOrDefault() ?? string.Empty;

            if (userDto.Role == "Admin") // Bỏ qua Admin
                return null;

            return userDto;
        }

        public async Task<ApiResponse<string>> UpdateUserProfileAsync(Guid userId, UpdateUserProfileRequestDto request)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null)
                return new ApiResponse<string>("User not found.");

            // Cập nhật thông tin cá nhân
            user.FullName = request.FullName ?? user.FullName;
            user.Phone = request.Phone ?? user.Phone;
            user.Gender = request.Gender ?? user.Gender;
            user.Address = request.Address ?? user.Address;
            user.Birthday = request.Birthday ?? user.Birthday;
            user.AvatarUrl = request.AvatarUrl ?? user.AvatarUrl;
            user.Status = request.Status ?? user.Status;

            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>(true, "User updated successfully.");
        }



        public async Task<bool> UpdateUserAsync(Guid userId, UpdateUserRequestDto request)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null) return false;

            _mapper.Map(request, user);

            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> UpdateAccountAsync(ChangeUserRoleRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return false;

            var roleName = request.Role.ToString(); // Chuyển enum thành string

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded) return false;

            var addResult = await _userManager.AddToRoleAsync(user, roleName);
            return addResult.Succeeded;
        }
        public async Task<UserDto> GetUserProfileAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not authenticated.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            // Lấy danh sách role của user
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "";

            // Mapping
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Role = role;
            return userDto;
        
        }
        public async Task<bool> DisableUserAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null)
                return false;

            if (user.Status == EntityStatus.Inactive)
            {
                // User is already disabled → no action needed, return true
                return true;
            }

            user.Status = EntityStatus.Inactive;
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> ActivateUserAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null)
                return false;

            if (user.Status == EntityStatus.Active)
            {
                // User is already active → no action needed, return true
                return true;
            }

            user.Status = EntityStatus.Active;
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<ApiResponse<string>> ChangePasswordAsync(ChangePasswordRequestDto request)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return new ApiResponse<string>("User is not authenticated.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ApiResponse<string>("User not found.");

            if (!await _userManager.CheckPasswordAsync(user, request.OldPassword))
                return new ApiResponse<string>("Current password is incorrect.");

            if (request.NewPassword != request.ConfirmedPassword)
                return new ApiResponse<string>("New password and confirmation do not match.");

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
                return new ApiResponse<string>("Password change failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return new ApiResponse<string>(string.Empty, "Password changed successfully.");
        }


        public async Task<bool> ChangeEmailAsync(ChangeEmailRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return false;

            var existing = await _userManager.FindByEmailAsync(request.NewEmail.ToLower());
            if (existing != null && existing.Id != user.Id) return false; // Email đã tồn tại

            user.Email = request.NewEmail.ToLower();
            user.UserName = request.NewEmail.ToLower(); // Nếu bạn đang dùng email làm username

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> CheckPasswordAsync(CheckPasswordRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return false;

            return await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
        }

    }
}
