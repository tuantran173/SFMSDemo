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
            var users = await _unitOfWork.AdminRepository.GetAllWithIncludesAsync(u => u.Role);
            var filteredUsers = users.Where(u => u.Role.Name != "Admin").ToList();
            return _mapper.Map<List<UserResponseDto>>(filteredUsers);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetByIdWithIncludesAsync(userId, u => u.Role);
            if (user == null || user.Role.Name == "Admin")
                return null;
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<bool> UpdateUserAsync(Guid userId, UpdateUserRequestDto request)
        {
            var user = await _unitOfWork.AdminRepository.GetByIdWithIncludesAsync(userId, u => u.Role);
            if (user == null || user.Role.Name == "Admin")
                return false;

            _mapper.Map(request, user);

            // Gọi phương thức UpdateUserAsync thay vì trả về bool từ Repository
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        // ✅ Thay đổi vai trò User ngoại trừ Admin
        public async Task<bool> ChangeUserRoleAsync(ChangeUserRoleRequestDto request)
        {
            var user = await _unitOfWork.AdminRepository.GetByIdWithIncludesAsync(request.UserId, u => u.Role);
            if (user == null || user.Role.Name == "Admin")
                return false;

            user.RoleId = request.NewRoleId; // Cập nhật RoleId trực tiếp

            // Gọi phương thức UpdateUserAsync thay vì trả về bool từ Repository
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        // ✅ Xóa User bằng cách vô hiệu hóa (Inactive) ngoại trừ Admin
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _unitOfWork.AdminRepository.GetByIdWithIncludesAsync(userId, u => u.Role);
            if (user == null || user.Role.Name == "Admin")
                return false;

            user.Status = EntityStatus.Inactive;

            // Gọi phương thức UpdateUserAsync thay vì trả về bool từ Repository
            await _unitOfWork.AdminRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
