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

namespace SFMSSolution.Application.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
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

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var userDto = _mapper.Map<UserResponseDto>(user);
            userDto.Role = roles.FirstOrDefault() ?? string.Empty;

            if (userDto.Role == "Admin") // Bỏ qua Admin
                return null;

            return userDto;
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

            var roleExists = await _roleManager.RoleExistsAsync(request.Role);
            if (!roleExists) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded) return false;

            var addResult = await _userManager.AddToRoleAsync(user, request.Role);
            return addResult.Succeeded;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return null;

            var dto = new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName ?? "",
                Email = user.Email ?? "",
                Phone = user.PhoneNumber ?? "",
                Address = user.Address ?? "",
                Birthday = user.Birthday,
                AvatarUrl = user.AvatarUrl ?? "",
                Gender = user.Gender.ToString()
            };

            return dto;
        }
        public async Task<bool> DisableUserAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null || user.Status == EntityStatus.Inactive)
                return false;

            user.Status = EntityStatus.Inactive;
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> ActivateUserAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null || user.Status == EntityStatus.Active)
                return false;

            user.Status = EntityStatus.Active;
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return false;

            // Xác minh mật khẩu hiện tại trước
            var isCorrect = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!isCorrect)
                throw new Exception("Current password is incorrect.");

            if (!string.Equals(request.NewPassword, request.ConfirmNewPassword))
                throw new Exception("New password and confirmation do not match.");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
                throw new Exception("Password change failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return true;
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
