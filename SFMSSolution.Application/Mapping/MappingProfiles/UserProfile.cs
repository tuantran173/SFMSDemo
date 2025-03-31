using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Admin;
using SFMSSolution.Domain.Entities;
using System.Linq;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Application.DataTransferObjects.User;
using SFMSSolution.Application.DataTransferObjects.User.Request;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status == EntityStatus.Active));
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status == EntityStatus.Active));
            CreateMap<UpdateUserRequestDto, User>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

        }
    }
}
