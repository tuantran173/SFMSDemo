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

namespace SFMSSolution.Application.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.AdminRepository.GetAllUsersAsync();
            // Loại bỏ tài khoản admin dựa trên collection UserRoles
            var filteredUsers = users.Where(u => !u.UserRoles.Any(ur => ur.Role.Name == "Admin")).ToList();
            return _mapper.Map<List<UserResponseDto>>(filteredUsers);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdAsync(userId);
            // Nếu user có role Admin, trả về null
            if (user != null && user.UserRoles.Any(ur => ur.Role.Name == "Admin"))
            {
                return null;
            }
            return user != null ? _mapper.Map<UserResponseDto>(user) : null;
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UpdateUserRequestDto request)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdAsync(userId);
            if (user == null)
                return false;

            // Không cho phép cập nhật tài khoản admin
            if (user.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                return false;

            // Map các thuộc tính từ request sang user
            _mapper.Map(request, user);

            var result = await _unitOfWork.AdminRepository.UpdateUserAsync(user);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
            }
            return result;
        }

        public async Task<bool> ChangeUserRoleAsync(ChangeUserRoleRequestDto request)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdAsync(request.UserId);
            // Không cho phép thay đổi role của tài khoản admin
            if (user != null && user.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                return false;

            var result = await _unitOfWork.AdminRepository.ChangeUserRoleAsync(request.UserId, request.NewRoleId);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
            }
            return result;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdAsync(userId);
            if (user == null)
                return false;

            // Không cho phép xóa tài khoản admin
            if (user.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                return false;

            // Thay vì xóa, chuyển trạng thái tài khoản sang Inactive
            user.Status = EntityStatus.Inactive;
            var result = await _unitOfWork.AdminRepository.UpdateUserAsync(user);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
            }
            return result;
        }
    }
}
