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

        public async Task<(List<UserResponseDto> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var (users, totalCount) = await _unitOfWork.AdminRepository.GetAllUsersWithRolesAsync(pageNumber, pageSize);

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

        public async Task<bool> ChangeUserRoleAsync(ChangeUserRoleRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return false;

            var newRole = await _roleManager.FindByIdAsync(request.NewRoleId.ToString());
            if (newRole == null) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            var roleResult = await _userManager.AddToRoleAsync(user, newRole.Name);

            return roleResult.Succeeded;
        }


        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null) return false;

            user.Status = EntityStatus.Inactive;
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequestDto request)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                throw new Exception("Current password is incorrect.");

            if (!string.Equals(request.NewPassword, request.ConfirmNewPassword))
                throw new Exception("New password and confirmation do not match.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _unitOfWork.AdminRepository.UpdateAsync(user); // Không cần gán kết quả

            await _unitOfWork.CompleteAsync(); // Lưu thay đổi vào CSDL

            return true;
        }
    }
}
