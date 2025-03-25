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

        public async Task<(List<UserResponseDto> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var (users, totalCount) = await _unitOfWork.AdminRepository.GetAllUsersWithRolesAsync(pageNumber, pageSize);
            var filteredUsers = users.Where(u => u.Role.Name != "Admin").ToList();
            var userDtos = _mapper.Map<List<UserResponseDto>>(filteredUsers);

            return (userDtos, totalCount);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null || user.Role.Name == "Admin")
                return null;

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UpdateUserRequestDto request)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null || user.Role.Name == "Admin")
                return false;

            _mapper.Map(request, user);

            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> ChangeUserRoleAsync(ChangeUserRoleRequestDto request)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(request.UserId);
            if (user == null || user.Role.Name == "Admin")
                return false;

            user.RoleId = request.NewRoleId;
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetUserByIdWithRolesAsync(userId);
            if (user == null || user.Role.Name == "Admin")
                return false;

            user.Status = EntityStatus.Inactive;
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
