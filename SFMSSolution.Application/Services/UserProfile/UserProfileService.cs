using AutoMapper;
using BCrypt.Net;
using SFMSSolution.Application.DataTransferObjects.User;
using SFMSSolution.Application.DataTransferObjects.User.Request;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;

namespace SFMSSolution.Application.Services.UserProfile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProfileResponseDto?> GetUserProfileAsync(Guid userId)
        {
            var user = await _unitOfWork.UserProfileRepository.GetUserByIdAsync(userId);
            return user != null ? _mapper.Map<ProfileResponseDto>(user) : null;
        }

        public async Task<bool> UpdateUserProfileAsync(Guid userId, UpdateProfileRequestDto request)
        {
            var user = await _unitOfWork.UserProfileRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            _mapper.Map(request, user);
            var result = await _unitOfWork.UserProfileRepository.UpdateUserAsync(user);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
            }
            return result;
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequestDto request)
        {
            var user = await _unitOfWork.UserProfileRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                throw new Exception("Current password is incorrect.");

            if (!string.Equals(request.NewPassword, request.ConfirmNewPassword))
                throw new Exception("New password and confirmation do not match.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            var result = await _unitOfWork.UserProfileRepository.UpdateUserAsync(user);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
            }
            return result;
        }
    }
}
