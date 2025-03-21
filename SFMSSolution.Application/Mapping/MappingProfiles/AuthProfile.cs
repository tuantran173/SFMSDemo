using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Auth;
using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Domain.Entities;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, AuthResponseDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : string.Empty));

            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Hash mật khẩu sau
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID tự động sinh
                .ForMember(dest => dest.RoleId, opt => opt.Ignore()); // RoleId có thể được set sau
        }
    }
}
